using Asp.Versioning;
using CarMarket.Server.ActionFilters;
using CarMarket.Server.Extensions;
using CarMarket.Server.Helpers;
using CarMarket.Server.Utility;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Logging;
using NLog;
using Repository.DataShaping;

namespace CarMarket.Server;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		LogManager.Setup().LoadConfigurationFromFile("nlog.config", true);
		ConfigureServices(builder.Services, builder.Configuration);

		var app = builder.Build();

		ConfigureApp(app);

		app.MapControllers();
		app.Run();
	}

	public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
	{
		services.ConfigureLoggerService();
		services.ConfigureCors();
		services.ConfigureIISIntegration();
		services.ConfigureSqlContext(configuration);
		services.ConfigureRepositoryManager();
		services.ConfigureAutoMapping();
		services.ConfigureApiBehaviorOptions();
		services.AddScoped<ValidationFilterAttribute>();
		services.AddScoped<ValidateCarShopExistsAttribute>();
		services.AddScoped<ValidateCarForCarShopExistsAttribute>();
		services.AddScoped<IDataShaper<CarDto>, DataShaper<CarDto>>();
		services.AddScoped<ValidateMediaTypeAttribute>();
		services.AddScoped<CarLinks>();
		services.ConfigureVersioning();

		services.AddControllers(config =>
		{
			config.RespectBrowserAcceptHeader = true;
			config.ReturnHttpNotAcceptable = true;
		}).AddNewtonsoftJson()
		.AddXmlDataContractSerializerFormatters()
		.AddCustomCSVFormatter();

		services.AddCustomMediaTypes();
	}

	public static void ConfigureApp(IApplicationBuilder app)
	{

		app.ConfigureExceptionMiddleware();
		app.UseHttpsRedirection();

		app.UseCors("CorsPolicy");

		app.UseForwardedHeaders(new ForwardedHeadersOptions
		{
			ForwardedHeaders = ForwardedHeaders.All
		});

		app.UseRouting();
		app.UseAuthorization();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
		});
	}
}
