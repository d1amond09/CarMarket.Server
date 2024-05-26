using Contracts;
using System;
using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects;
using AutoMapper;
using CarMarket.Server.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Entities.Models;
using System.ComponentModel.Design;

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

	[HttpPost]
	public IActionResult CreateCarShop(Guid addressId, [FromBody] CarShopForCreationDto carShop)
	{
		if (carShop == null)
		{
			_logger.LogError("CarShopForCreationDto object sent from client is null.");
			return BadRequest("CarShopForCreationDto object is null");
		}

		var address = _repository.Address.GetAddress(addressId, trackChanges: false);
		if (address == null)
		{
			_logger.LogInfo($"Address with id: {address} doesn't exist in the database.");
			return NotFound();
		}

		var carShopEntity = _mapper.Map<CarShop>(carShop);
		_repository.CarShop.CreateCarShop(addressId, carShopEntity);
		_repository.Save();
		var carShopToReturn = _mapper.Map<CarShopDto>(carShopEntity);
		return CreatedAtRoute("GetCarShopById", new { id = carShopToReturn.Id }, carShopToReturn);
	}

}