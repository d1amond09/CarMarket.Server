using System.ComponentModel.Design;
using AutoMapper;
using CarMarket.Server.ActionFilters;
using CarMarket.Server.Utility;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarMarket.Server.Controllers;

[Route("api/carShops/{carShopId}/cars")]
[ApiController]
public class CarsController(CarLinks carLinks,
							IDataShaper<CarDto> dataShaper,
							IRepositoryManager repository, 
							ILoggerManager logger, 
							IMapper mapper) : ControllerBase
{
	private readonly CarLinks _carLinks = carLinks;
	private readonly IDataShaper<CarDto> _dataShaper = dataShaper;
	private readonly IRepositoryManager _repository = repository;
	private readonly ILoggerManager _logger = logger;
	private readonly IMapper _mapper = mapper;

	[HttpGet]
	[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
	public async Task<IActionResult> GetCarsForCarShop(Guid carShopId, [FromQuery] CarParameters carParameters)
	{
		if (!carParameters.ValidAgeRange)
			return BadRequest("Max price can't be less than min price.");

		var carShop = _repository.CarShop.GetCarShop(carShopId, trackChanges: false);
		if (carShop == null)
		{
			_logger.LogInfo($"CarShop with id: {carShopId} doesn't exist in the database.");
			return NotFound();
		}
		var carsFromDb = await _repository.Car.GetCarsAsync(carShopId, carParameters, trackChanges: false);

		Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(carsFromDb.MetaData));

		var carsDto = _mapper.Map<IEnumerable<CarDto>>(carsFromDb);

		var links = _carLinks.TryGenerateLinks(carsDto, carParameters.Fields, carShopId, HttpContext);
		return links.HasLinks ? Ok(links.LinkedEntities) : Ok(links.ShapedEntities);
	}

	[HttpGet("{id}", Name = "GetCarById")]
	public async Task<IActionResult> GetCarForCarShop(Guid carShopId, Guid id)
	{
		var carShop = _repository.CarShop.GetCarShop(carShopId, trackChanges: false);
		if (carShop == null)
		{
			_logger.LogInfo($"CarShop with id: {carShopId} doesn't exist in the database.");
			return NotFound();
		}

		var carFromDb = await _repository.Car.GetCarAsync(carShopId, id, trackChanges: false);
		if (carFromDb == null)
		{
			_logger.LogInfo($"Car with id: {id} doesn't exist in the database.");
			return NotFound();
		}
		var car = _mapper.Map<CarDto>(carFromDb);
		return Ok(car);
	}

	[HttpPost]
	[ServiceFilter(typeof(ValidationFilterAttribute))]
	public async Task<IActionResult> CreateCarForCarShop(Guid carShopId, [FromBody] CarForManipulationDto car)
	{
		var carShop = await _repository.CarShop.GetCarShopAsync(carShopId, trackChanges: false);
		if (carShop == null)
		{
			_logger.LogInfo($"CarShop with id: {carShopId} doesn't exist in the database.");
			return NotFound();
		}

		var carEntity = _mapper.Map<Car>(car);
		_repository.Car.CreateCar(carShopId, carEntity);
		await _repository.SaveAsync();
		var carToReturn = _mapper.Map<CarDto>(carEntity);
		return CreatedAtRoute("GetCarById", new { carShopId, id = carToReturn.Id }, carToReturn);
	}

	[HttpDelete("{id}")]
	[ServiceFilter(typeof(ValidateCarForCarShopExistsAttribute))]
	public async Task<IActionResult> DeleteCarForCarShop(Guid carShopId, Guid id)
	{
		if (HttpContext.Items["car"] is Car car)
		{
			_repository.Car.DeleteCar(car);
			await _repository.SaveAsync();
		}

		return NoContent();
	}

	[HttpPut("{id}")]
	[ServiceFilter(typeof(ValidationFilterAttribute))]
	[ServiceFilter(typeof(ValidateCarForCarShopExistsAttribute))]
	public async Task<IActionResult> UpdateCarForCarShop(Guid carShopId, Guid id,
		[FromBody] CarForUpdateDto car)
	{
		if (HttpContext.Items["car"] is Car carEntity)
		{
			_mapper.Map(car, carEntity);
			await _repository.SaveAsync();
		}

		return NoContent();
	}

	[HttpPatch("{id}")]
	[ServiceFilter(typeof(ValidateCarForCarShopExistsAttribute))]
	public async Task<IActionResult> PartiallyUpdateCarForCarShop(Guid carShopId, Guid id, 
		[FromBody] JsonPatchDocument<CarForUpdateDto> patchDoc)
	{
		if (patchDoc == null)
		{
			_logger.LogError("patchDoc object sent from client is null.");
			return BadRequest("patchDoc object is null");
		}

		if (HttpContext.Items["car"] is Car carEntity)
		{
			var employeeToPatch = _mapper.Map<CarForUpdateDto>(carEntity);
			patchDoc.ApplyTo(employeeToPatch);
			TryValidateModel(employeeToPatch);
			if (!ModelState.IsValid)
			{
				_logger.LogError("Invalid model state for the patch document");
				return UnprocessableEntity(ModelState);
			}

			_mapper.Map(employeeToPatch, carEntity);
			await _repository.SaveAsync();
		}
	
		return NoContent();
	}
}
