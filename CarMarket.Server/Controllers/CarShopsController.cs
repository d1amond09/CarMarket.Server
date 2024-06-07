using Contracts;
using System;
using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects;
using AutoMapper;
using Entities.Models;
using System.ComponentModel.Design;
using CarMarket.Server.ModelBinders;
using CarMarket.Server.ActionFilters;

namespace CarMarket.Server.Controllers;

[ApiController]
[Route("api/carShops")]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "v1")]
public class CarShopsController(IRepositoryManager repository, 
								ILoggerManager logger, 
								IMapper mapper) : ControllerBase
{
	private readonly IRepositoryManager _repository = repository;
	private readonly ILoggerManager _logger = logger;
	private readonly IMapper _mapper = mapper;

	[HttpOptions]
	public IActionResult GetCarShopsOptions()
	{
		Response.Headers.Append("Allow", "GET, OPTIONS, POST");
		return Ok();
	}

	[HttpGet(Name = "GetCarShops")]
	public async Task<IActionResult> GetCarShops()
	{
		var carShops = await _repository.CarShop.GetAllCarShopsAsync(trackChanges: false);

		var carShopsDto = _mapper.Map<IEnumerable<CarShopDto>>(carShops);

		return Ok(carShopsDto);
	}

	[HttpGet("{id}", Name = "GetCarShopById")]
	public async Task<IActionResult> GetCarShop(Guid id)
	{
		var carShop = await _repository.CarShop.GetCarShopAsync(id, trackChanges: false);
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
	public async Task<IActionResult> GetCarShopCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
	{
		if (ids == null)
		{
			_logger.LogError("Parameter ids is null");
			return BadRequest("Parameter ids is null");
		}
		var companyEntities = await _repository.CarShop.GetByIdsAsync(ids, trackChanges: false);
		if (ids.Count() != companyEntities.Count())
		{
			_logger.LogError("Some ids are not valid in a collection");
			return NotFound();
		}
		var companiesToReturn = _mapper.Map<IEnumerable<CarShopDto>>(companyEntities);
		return Ok(companiesToReturn);
	}

	[HttpPost(Name = "CreateCarShop")]
	[ServiceFilter(typeof(ValidationFilterAttribute))]
	public async Task<IActionResult> CreateCarShop([FromBody] CarShopForManipulationDto carShop)
	{
		var carShopEntity = _mapper.Map<CarShop>(carShop);
		_repository.CarShop.CreateCarShop(carShopEntity);

		await _repository.SaveAsync();

		var carShopToReturn = _mapper.Map<CarShopDto>(carShopEntity);
		return CreatedAtRoute("GetCarShopById", new { id = carShopToReturn.Id }, carShopToReturn);
	}

	[HttpPost("collection")]
	public async Task<IActionResult> CreatecarShopCollection([FromBody] IEnumerable<CarShopForCreationDto> carShopCollection)
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
		await _repository.SaveAsync();
		var carShopCollectionToReturn = _mapper.Map<IEnumerable<CarShopDto>>(companyEntities);
		var ids = string.Join(",", carShopCollectionToReturn.Select(c => c.Id));
		return CreatedAtRoute("carShopCollection", new { ids },
	   carShopCollectionToReturn);
	}


	[HttpDelete("{id}")]
	[ServiceFilter(typeof(ValidateCarShopExistsAttribute))]
	public async Task<IActionResult> DeleteCarShop(Guid id)
	{
		if(HttpContext.Items["carShop"] is CarShop carShop)
		{
			_repository.CarShop.DeleteCarShop(carShop);
		
			await _repository.SaveAsync();
		}
		return NoContent();
	}

	[HttpPut("{id}")]
	[ServiceFilter(typeof(ValidationFilterAttribute))]
	[ServiceFilter(typeof(ValidateCarShopExistsAttribute))]
	public async Task<IActionResult> UpdateCarShop(Guid id, [FromBody] CarShopForUpdateDto carShop)
	{
		if (HttpContext.Items["carShop"] is CarShop carShopEntity)
		{
			_mapper.Map(carShop, carShopEntity);

			await _repository.SaveAsync();
		}
		return NoContent();
	}
}