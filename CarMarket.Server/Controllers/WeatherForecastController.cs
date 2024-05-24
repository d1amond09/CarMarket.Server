using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarMarket.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly IRepositoryManager _repository;
		public WeatherForecastController(IRepositoryManager repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			//_repository.Car.AnyMethodFromCarRepository();
			//_repository.CarShop.AnyMethodFromCarShopRepository();
			return new string[] { "value1", "value2" };
		}
	}
}
