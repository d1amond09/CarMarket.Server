﻿using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CarMarket.Server.Controllers;

[Route("api/carShops/{carShopId}/cars")]
[ApiController]
public class CarsController(IRepositoryManager repository, 
							ILoggerManager logger, 
							IMapper mapper) : ControllerBase
{
	private readonly IRepositoryManager _repository = repository;
	private readonly ILoggerManager _logger = logger;
	private readonly IMapper _mapper = mapper;

	[HttpGet]
	public IActionResult GetCarsForCarShop(Guid carShopId)
	{
		var carShop = _repository.CarShop.GetCarShop(carShopId, trackChanges: false);
		if (carShop == null)
		{
			_logger.LogInfo($"CarShop with id: {carShopId} doesn't exist in the database.");
			return NotFound();
		}
		var carsFromDb = _repository.Car.GetCars(carShopId, trackChanges: false);

		var carsDto = _mapper.Map<IEnumerable<CarDto>>(carsFromDb);
		return Ok(carsDto);
	}

	[HttpGet("{id}", Name = "GetCarById")]
	public IActionResult GetCarForCompany(Guid carShopId, Guid id)
	{
		var carShop = _repository.CarShop.GetCarShop(carShopId, trackChanges: false);
		if (carShop == null)
		{
			_logger.LogInfo($"CarShop with id: {carShopId} doesn't exist in the database.");
			return NotFound();
		}

		var carFromDb = _repository.Car.GetCar(carShopId, id, trackChanges: false);
		if (carFromDb == null)
		{
			_logger.LogInfo($"Car with id: {id} doesn't exist in the database.");
			return NotFound();
		}
		var car = _mapper.Map<CarDto>(carFromDb);
		return Ok(car);
	}

	[HttpPost]
	public IActionResult CreateCarShop(Guid carShopId, [FromBody] CarForManipulationDto car)
	{
		if (car == null)
		{
			_logger.LogError("CarForCreationDto object sent from client is null.");
			return BadRequest("CarForCreationDto object is null");
		}

		if (!ModelState.IsValid)
		{
			_logger.LogError("Invalid model state for the CarForCreationDto object");
			return UnprocessableEntity(ModelState);
		}

		var carShop = _repository.CarShop.GetCarShop(carShopId, trackChanges: false);
		if (carShop == null)
		{
			_logger.LogInfo($"CarShop with id: {carShopId} doesn't exist in the database.");
			return NotFound();
		}

		var carEntity = _mapper.Map<Car>(car);
		_repository.Car.CreateCar(carShopId, carEntity);
		_repository.Save();
		var carToReturn = _mapper.Map<CarDto>(carEntity);
		return CreatedAtRoute("GetCarById", new { carShopId, id = carToReturn.Id }, carToReturn);
	}

	[HttpDelete("{id}")]
	public IActionResult DeleteCarcase(Guid carShopId, Guid id)
	{
		var carShop = _repository.CarShop.GetCarShop(carShopId, trackChanges: false);
		if (carShop == null)
		{
			_logger.LogInfo($"CarShop with id: {carShopId} doesn't exist in the database.");
			return NotFound();
		}

		var car = _repository.Car.GetCar(carShopId, id, trackChanges: false);
		if (car == null)
		{
			_logger.LogInfo($"Car with id: {id} doesn't exist in the database.");
			return NotFound();
		}
		_repository.Car.DeleteCar(car);
		_repository.Save();
		return NoContent();
	}

	[HttpPut("{id}")]
	public IActionResult UpdateCarForCarShop(Guid carShopId, Guid id,
		[FromBody] CarForUpdateDto car)
	{
		if (car == null)
		{
			_logger.LogError("CarForUpdateDto object sent from client is null.");
			return BadRequest("CarForUpdateDto object is null");
		}

		var carShop = _repository.CarShop.GetCarShop(carShopId, trackChanges: false);
		if (carShop == null)
		{
			_logger.LogInfo($"CarShop with id: {carShopId} doesn't exist in the database.");
			return NotFound();
		}

		var carEntity = _repository.Car.GetCar(carShopId, id, trackChanges: true);
		if (carEntity == null)
		{
			_logger.LogInfo($"Car with id: {id} doesn't exist in the database.");
			return NotFound();
		}
		_mapper.Map(car, carEntity);
		_repository.Save();
		return NoContent();
	}

	[HttpPatch("{id}")]
	public IActionResult PartiallyUpdateCarForCarShop(Guid carShopId, Guid id, 
		[FromBody] JsonPatchDocument<CarForUpdateDto> patchDoc)
	{
		if (patchDoc == null)
		{
			_logger.LogError("patchDoc object sent from client is null.");
			return BadRequest("patchDoc object is null");
		}
		var company = _repository.CarShop.GetCarShop(carShopId, trackChanges: false);
		if (company == null)
		{
			_logger.LogInfo($"CarShop with id: {carShopId} doesn't exist in the database.");
			return NotFound();
		}
		var carEntity = _repository.Car.GetCar(carShopId, id, trackChanges: true);
		if (carEntity == null)
		{
			_logger.LogInfo($"Car with id: {id} doesn't exist in the database.");
			return NotFound();
		}
		var employeeToPatch = _mapper.Map<CarForUpdateDto>(carEntity);
		patchDoc.ApplyTo(employeeToPatch);
		_mapper.Map(employeeToPatch, carEntity);
		_repository.Save();
		return NoContent();
	}
}
