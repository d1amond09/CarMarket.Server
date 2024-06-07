using Contracts;
using Entities;
using LoggerService;
using Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Entities.DataTransferObjects;
using Entities.Models;
using CarMarket.Server.Formatters.Output;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using CarMarket.Server.Controllers;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace CarMarket.Server.Extensions;

public static class ServiceExtensions
{
	public static void ConfigureCors(this IServiceCollection services) =>
		services.AddCors(options => 
		{
			options.AddPolicy("CorsPolicy", builder =>
			builder.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader());
		});

	public static void ConfigureIISIntegration(this IServiceCollection services) =>
		services.Configure<IISOptions>(options =>
		{

		});

	public static void ConfigureLoggerService(this IServiceCollection services) =>
		services.AddSingleton<ILoggerManager, LoggerManager>();

	public static void ConfigureSqlContext(this IServiceCollection services,
		IConfiguration configuration) =>
		services.AddDbContext<RepositoryContext>(opts =>
		opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b =>
		b.MigrationsAssembly("CarMarket.Server")));

	public static void ConfigureRepositoryManager(this IServiceCollection services) =>
		services.TryAddScoped<IRepositoryManager, RepositoryManager>();

	public static void ConfigureAutoMapping(this IServiceCollection services)
	{

		services.AddAutoMapper(x => x.CreateMap<CarShop, CarShopDto>()
				.ForMember(c => c.FullAddress, opt =>
					opt.MapFrom(x => string.Join(' ', x.Country, x.Address))));
		services.AddAutoMapper(x => x.CreateMap<CarShopForUpdateDto, CarShop>());
		services.AddAutoMapper(x => x.CreateMap<CarShopForManipulationDto, CarShop>());

		services.AddAutoMapper(x => x.CreateMap<Car, CarDto>());
		services.AddAutoMapper(x => x.CreateMap<CarForManipulationDto, Car>());
		services.AddAutoMapper(x => x.CreateMap<CarForUpdateDto, Car>().ReverseMap());
	}

	public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) =>
		builder.AddMvcOptions(config => 
			config.OutputFormatters.Add(new CsvOutputFormatter()));

	public static void ConfigureApiBehaviorOptions(this IServiceCollection services) =>
		services.Configure<ApiBehaviorOptions>(options =>
		{
			options.SuppressModelStateInvalidFilter = true;
		});

	public static void AddCustomMediaTypes(this IServiceCollection services)
	{
		services.Configure<MvcOptions>(config =>
		{
			var newtonsoftJsonOutputFormatter = config.OutputFormatters
				.OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();

			newtonsoftJsonOutputFormatter?.SupportedMediaTypes
				.Add("application/vnd.codemaze.hateoas+json");

			newtonsoftJsonOutputFormatter?.SupportedMediaTypes
				.Add("application/vnd.codemaze.apiroot+json");

			var xmlOutputFormatter = config.OutputFormatters
				.OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();

			xmlOutputFormatter?.SupportedMediaTypes
				.Add("application/vnd.codemaze.hateoas+xml");

			xmlOutputFormatter?.SupportedMediaTypes
				.Add("application/vnd.codemaze.apiroot+xml");
		});
	}

	public static void ConfigureVersioning(this IServiceCollection services)
	{
		services.AddEndpointsApiExplorer();

		services.AddApiVersioning(opt =>
			{
				opt.ReportApiVersions = true;
				opt.AssumeDefaultVersionWhenUnspecified = true;
				opt.DefaultApiVersion = new ApiVersion(1, 0);
				opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
				opt.Conventions.Controller<CarShopsController>()
					.HasApiVersion(new ApiVersion(1, 0));
				opt.Conventions.Controller<CarShopsV2Controller>()
					.HasDeprecatedApiVersion(new ApiVersion(2, 0));
			}
		);
	}


}
