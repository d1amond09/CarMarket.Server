using Contracts;
using System;
using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects;
using AutoMapper;
using CarMarket.Server.Exceptions;

namespace CarMarket.Server.Controllers
{
	[Route("api/carShops")]
	[ApiController]
	public class CarShopsController : ControllerBase
	{
		private readonly IRepositoryManager _repository;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;

		public CarShopsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
		{
			_repository = repository;
			_logger = logger;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetCarShops()
		{
			var carShops = _repository.CarShop.GetAllCarShops(trackChanges: false);

			var carShopsDto = _mapper.Map<IEnumerable<CarShopDto>>(carShops);

			return Ok(carShopsDto);
		}

		[HttpGet("{id}")]
		public IActionResult GetCarShop(Guid id)
		{
			var carShop = _repository.CarShop.GetCarShop(id, trackChanges: false);
			if (carShop == null)
			{
				_logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
				return NotFound();
			}
			else
			{
				var carShopDto = _mapper.Map<CarShopDto>(carShop);
				return Ok(carShopDto);
			}
		}
	}
}
