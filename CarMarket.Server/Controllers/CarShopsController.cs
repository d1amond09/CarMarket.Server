using Contracts;
using System;
using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects;
using AutoMapper;
using CarMarket.Server.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Entities.Models;
using System.ComponentModel.Design;
using CarMarket.Server.ModelBinders;

namespace CarMarket.Server.Controllers;

[Route("api/carShops")]
[ApiController]
public class CarShopsController(IRepositoryManager repository, 
								ILoggerManager logger, 
								IMapper mapper) : ControllerBase
{
	private readonly IRepositoryManager _repository = repository;
	private readonly ILoggerManager _logger = logger;
	private readonly IMapper _mapper = mapper;

	[HttpGet]
	public IActionResult GetCarShops()
	{
		var carShops = _repository.CarShop.GetAllCarShops(trackChanges: false);

		var carShopsDto = _mapper.Map<IEnumerable<CarShopDto>>(carShops);

		return Ok(carShopsDto);
	}

	[HttpGet("{id}", Name = "GetCarShopById")]
	public IActionResult GetCarShop(Guid id)
	{
		var carShop = _repository.CarShop.GetCarShop(id, trackChanges: false);
		if (carShop == null)
		{
			_logger.LogInfo($"CarShop with id: {id} doesn't exist in the database.");
			return NotFound();
		}
		else
		{
			var carShopDto = _mapper.Map<CarShopDto>(carShop);
			return Ok(carShopDto);
		}
	}

	[HttpGet("collection/({ids})", Name = "CarShopCollection")]
	public IActionResult GetCarShopCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
	{
		if (ids == null)
		{
			_logger.LogError("Parameter ids is null");
			return BadRequest("Parameter ids is null");
		}
		var companyEntities = _repository.CarShop.GetByIds(ids, trackChanges: false);
		if (ids.Count() != companyEntities.Count())
		{
			_logger.LogError("Some ids are not valid in a collection");
			return NotFound();
		}
		var companiesToReturn = _mapper.Map<IEnumerable<CarShopDto>>(companyEntities);
		return Ok(companiesToReturn);
	}


	[HttpPost]
	public IActionResult CreateCarShop([FromBody] CarShopForManipulationDto carShop)
	{
		if (carShop == null)
		{
			_logger.LogError("CarShopForCreationDto object sent from client is null.");
			return BadRequest("CarShopForCreationDto object is null");
		}

		if (!ModelState.IsValid)
		{
			_logger.LogError("Invalid model state for the CarShopForCreationDto object");
			return UnprocessableEntity(ModelState);
		}

		var carShopEntity = _mapper.Map<CarShop>(carShop);
		_repository.CarShop.CreateCarShop(carShopEntity);
		_repository.Save();
		var carShopToReturn = _mapper.Map<CarShopDto>(carShopEntity);
		return CreatedAtRoute("GetCarShopById", new { id = carShopToReturn.Id }, carShopToReturn);
	}

	[HttpPost("collection")]
	public IActionResult CreatecarShopCollection([FromBody] IEnumerable<CarShopForCreationDto> carShopCollection)
	{
		if (carShopCollection == null)
		{
			_logger.LogError("Company collection sent from client is null.");
			return BadRequest("Company collection is null");
		}
		var companyEntities = _mapper.Map<IEnumerable<CarShop>>(carShopCollection);
		foreach (var company in companyEntities)
		{
			_repository.CarShop.CreateCarShop(company);
		}
		_repository.Save();
		var carShopCollectionToReturn =
	   _mapper.Map<IEnumerable<CarShopDto>>(companyEntities);
		var ids = string.Join(",", carShopCollectionToReturn.Select(c => c.Id));
		return CreatedAtRoute("carShopCollection", new { ids },
	   carShopCollectionToReturn);
	}


	[HttpDelete("{id}")]
	public IActionResult DeleteCarShop(Guid id)
	{
		var carShop = _repository.CarShop.GetCarShop(id, trackChanges: false);
		if (carShop == null)
		{
			_logger.LogInfo($"CarShop with id: {id} doesn't exist in the database.");
			return NotFound();
		}
		_repository.CarShop.DeleteCarShop(carShop);
		_repository.Save();
		return NoContent();
	}

	[HttpPut("{id}")]
	public IActionResult UpdateCarShop(Guid id, [FromBody] CarShopForUpdateDto carShop)
	{
		if (carShop == null)
		{
			_logger.LogError("CarShopForUpdateDto object sent from client is null.");
			return BadRequest("CarShopForUpdateDto object is null");
		}
		var carShopEntity = _repository.CarShop.GetCarShop(id, trackChanges: true);
		if (carShopEntity == null)
		{
			_logger.LogInfo($"CarShop with id: {id} doesn't exist in the database.");
			return NotFound();
		}
		_mapper.Map(carShop, carShopEntity);
		_repository.Save();
		return NoContent();
	}
}