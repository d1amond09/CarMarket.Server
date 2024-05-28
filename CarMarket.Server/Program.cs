using CarMarket.Server.Extensions;
using CarMarket.Server.Helpers;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Logging;
using NLog;

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

		services.AddControllers(config =>
		{
			config.RespectBrowserAcceptHeader = true;
			config.ReturnHttpNotAcceptable = true;
		}).AddNewtonsoftJson()
		.AddXmlDataContractSerializerFormatters()
		.AddCustomCSVFormatter();

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
