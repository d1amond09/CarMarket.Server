using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
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
	public IActionResult CreateCarShop(Guid carShopId, Guid brandId, Guid carcaseId, [FromBody] CarForCreationDto car)
	{
		if (car == null)
		{
			_logger.LogError("CarForCreationDto object sent from client is null.");
			return BadRequest("CarForCreationDto object is null");
		}

		var carShop = _repository.CarShop.GetCarShop(carShopId, trackChanges: false);
		if (carShop == null)
		{
			_logger.LogInfo($"CarShop with id: {carShopId} doesn't exist in the database.");
			return NotFound();
		}

		var brand = _repository.Brand.GetBrand(brandId, trackChanges: false);
		if (brand == null)
		{
			_logger.LogInfo($"Brand with id: {brandId} doesn't exist in the database.");
			return NotFound();
		}

		var carcase = _repository.Carcase.GetCarcase(carcaseId, trackChanges: false);
		if (carcase == null)
		{
			_logger.LogInfo($"Carcase with id: {carcaseId} doesn't exist in the database.");
			return NotFound();
		}


		var carEntity = _mapper.Map<Car>(car);
		_repository.Car.CreateCar(carShopId, brandId, carcaseId, carEntity);
		_repository.Save();
		var carToReturn = _mapper.Map<CarDto>(carEntity);
		return CreatedAtRoute("GetCarById", new { carShopId, id = carToReturn.Id }, carToReturn);
	}
}
