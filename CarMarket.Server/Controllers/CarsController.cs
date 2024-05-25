using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarMarket.Server.Controllers;

[Route("api/carShops/{carShopId}/cars")]
[ApiController]
public class CarsController : ControllerBase
{
	private readonly IRepositoryManager _repository;
	private readonly ILoggerManager _logger;
	private readonly IMapper _mapper;

	public CarsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
	{
		_repository = repository;
		_logger = logger;
		_mapper = mapper;
	}

	[HttpGet]
	public IActionResult GetCarsForCarShop(Guid carShopId)
	{
		var carShop = _repository.CarShop.GetCarShop(carShopId, trackChanges: false);
		if (carShop == null)
		{
			_logger.LogInfo($"Company with id: {carShopId} doesn't exist in the database.");
			return NotFound();
		}
		var carsFromDb = _repository.Car.GetCars(carShopId, trackChanges: false);

		var carsDto = _mapper.Map<IEnumerable<CarDto>>(carsFromDb);
		return Ok(carsDto);
	}

	[HttpGet("{id}")]
	public IActionResult GetCarForCompany(Guid carShopId, Guid id)
	{
		var carShop = _repository.CarShop.GetCarShop(carShopId, trackChanges: false);
		if (carShop == null)
		{
			_logger.LogInfo($"Company with id: {carShopId} doesn't exist in the database.");
			return NotFound();
		}

		var carFromDb = _repository.Car.GetCar(carShopId, id, trackChanges: false);
		if (carFromDb == null)
		{
			_logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
			return NotFound();
		}
		var car = _mapper.Map<CarDto>(carFromDb);
		return Ok(car);
	}
}
