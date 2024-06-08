using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarMarket.Server.Controllers;

[ApiController]
[Route("api/carShops")]
public class CarShopsV2Controller(IRepositoryManager repository) : ControllerBase
{
	private readonly IRepositoryManager _repository = repository;

	[HttpGet]
	public async Task<IActionResult> GetCarShops()
	{
		var CarShops = await _repository.CarShop.GetAllCarShopsAsync(trackChanges: false);
		return Ok(CarShops);
	}
}
