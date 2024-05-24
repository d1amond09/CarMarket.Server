using Contracts;
using System;
using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects;
using AutoMapper;

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
			try
			{
				var carShops = _repository.CarShop.GetAllCarShops(trackChanges: false);

				var carShopsDto = _mapper.Map<IEnumerable<CarShopDto>>(carShops);

				return Ok(carShopsDto);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong in the {nameof(GetCarShops)} action {ex}");
				return StatusCode(500, "Internal server error");
			}
		}

	}
}
