using Contracts;
using Entities;
using LoggerService;
using Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Entities.DataTransferObjects;
using Entities.Models;
using CarMarket.Server.Formatters.Output;

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
		services.AddAutoMapper(x => x.CreateMap<CarShop, CarShopDto>());
		services.AddAutoMapper(x => x.CreateMap<CarShopForCreationDto, CarShop>());
		//		.ForMember(c => c.Name, opt =>
		//			opt.MapFrom(x => string.Join(' ', "Name:", x.Name))));

		services.AddAutoMapper(x => x.CreateMap<Car, CarDto>());
		services.AddAutoMapper(x => x.CreateMap<CarForCreationDto, Car>());
		services.AddAutoMapper(x => x.CreateMap<CarcaseForCreationDto, Car>());
		services.AddAutoMapper(x => x.CreateMap<Address, AddressDto>());
		services.AddAutoMapper(x => x.CreateMap<AddressForCreationDto, Address>());
		services.AddAutoMapper(x => x.CreateMap<Country, CountryDto>());
		services.AddAutoMapper(x => x.CreateMap<CountryForCreationDto, Country>());
		services.AddAutoMapper(x => x.CreateMap<Brand, BrandDto>());
		services.AddAutoMapper(x => x.CreateMap<BrandForCreationDto, Brand>());
		services.AddAutoMapper(x => x.CreateMap<Carcase, CarcaseDto>());
		services.AddAutoMapper(x => x.CreateMap<CarcaseForCreationDto, Carcase>());
	}

	public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) =>
		builder.AddMvcOptions(config => 
			config.OutputFormatters.Add(new CsvOutputFormatter()));

}
