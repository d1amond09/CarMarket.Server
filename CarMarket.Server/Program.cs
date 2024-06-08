using Asp.Versioning;
using AspNetCoreRateLimit;
using AuthenticationService;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
		services.AddScoped<IAuthenticationManager, AuthenticationManager>();

		services.ConfigureVersioning();

		services.ConfigureResponseCaching();
		services.ConfigureHttpCacheHeaders();
		services.AddMemoryCache();

		services.ConfigureRateLimitingOptions();
		services.AddHttpContextAccessor();

		services.AddAuthentication();
		services.ConfigureIdentity();
		services.ConfigureJWT(configuration);

		services.AddControllers(config =>
		{
			config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
			{
				Duration = 120
			});

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

		app.UseIpRateLimiting();
		app.UseCors("CorsPolicy");
		app.UseResponseCaching();
		app.UseHttpCacheHeaders();

		app.UseForwardedHeaders(new ForwardedHeadersOptions
		{
			ForwardedHeaders = ForwardedHeaders.All
		});

		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
		});
	}
}
