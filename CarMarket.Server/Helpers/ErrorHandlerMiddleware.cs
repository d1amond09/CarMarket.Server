using System.Net;
using System.Text.Json;
using CarMarket.Server.Exceptions;
using Contracts;
using Entities.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;

namespace CarMarket.Server.Helpers;

public class ErrorExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
{
	private readonly RequestDelegate _next = next;
	private readonly ILoggerManager _logger = logger;

	public async Task InvokeAsync(HttpContext httpContext)
	{
		try
		{
			await _next(httpContext);
		}
		catch (Exception ex)
		{
			_logger.LogError($"Something went wrong: {ex}");
			await HandleExceptionAsync(httpContext, ex);
		}
	}

	private async Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

		await context.Response.WriteAsync(new ErrorDetails()
		{
			StatusCode = context.Response.StatusCode,
			Message = $"Internal Server Error from the custom middleware. {exception.Message}"
		}.ToString());
	}
}
