using System.ComponentModel.Design;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarMarket.Server.ActionFilters;

public class ValidateCarForCarShopExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
{
	private readonly IRepositoryManager _repository = repository;
	private readonly ILoggerManager _logger = logger;

	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		var method = context.HttpContext.Request.Method;
		var trackChanges = method.Equals("PUT") || method.Equals("PATCH");
		var carShopId = (Guid) context.ActionArguments["carShopId"];
		var carShop = await _repository.CarShop.GetCarShopAsync(carShopId, false);
		if (carShop == null)
		{
			_logger.LogInfo($"CarShop with id: {carShopId} doesn't exist in the database.");
			context.Result = new NotFoundResult();
			return;
		}
		var id = (Guid) context.ActionArguments["id"];
		var car = await _repository.Car.GetCarAsync(carShopId, id, trackChanges);
		if (car == null)
		{
			_logger.LogInfo($"Car with id: {id} doesn't exist in the database.");
			context.Result = new NotFoundResult();
		}
		else
		{
			context.HttpContext.Items.Add("car", car);
			await next();
		}
	}
}

