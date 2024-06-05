using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarMarket.Server.ActionFilters;

public class ValidateCarShopExistsAttribute(IRepositoryManager repository, ILoggerManager logger) : IAsyncActionFilter
{
	private readonly IRepositoryManager _repository = repository;
	private readonly ILoggerManager _logger = logger;

	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
		var id = (Guid) context.ActionArguments["id"];
		var carShop = await _repository.CarShop.GetCarShopAsync(id, trackChanges);
		if (carShop == null)
		{
			_logger.LogInfo($"CarShop with id: {id} doesn't exist in the database.");
			context.Result = new NotFoundResult();
		}
		else
		{
			context.HttpContext.Items.Add("carShop", carShop);
			await next();
		}
	}
}
