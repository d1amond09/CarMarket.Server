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
using Marvin.Cache.Headers;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;

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

		services.AddAutoMapper(x => x.CreateMap<UserForRegistrationDto, User>());
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
				//opt.ApiVersionReader = new QueryStringApiVersionReader("api-version");
				opt.Conventions.Controller<CarShopsController>()
					.HasApiVersion(new ApiVersion(1, 0));
				opt.Conventions.Controller<CarShopsV2Controller>()
					.HasDeprecatedApiVersion(new ApiVersion(2, 0));
			}
		);
	}

	public static void ConfigureResponseCaching(this IServiceCollection services) =>
		services.AddResponseCaching();

	public static void ConfigureHttpCacheHeaders(this IServiceCollection services) =>
		services.AddHttpCacheHeaders((expirationOpt) =>
		{
			expirationOpt.MaxAge = 65;
			expirationOpt.CacheLocation = CacheLocation.Private;
		},
		(validationOpt) =>
		{
			validationOpt.MustRevalidate = true;
		});

	public static void ConfigureRateLimitingOptions(this IServiceCollection services)
	{
		var rateLimitRules = new List<RateLimitRule> {	
			new() {
				Endpoint = "*",
				Limit = 30,
				Period = "5s"
			}
		};

		services.Configure<IpRateLimitOptions>(opt => {
			opt.GeneralRules = rateLimitRules;
		});

		services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
		services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
		services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
		services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
	}

	public static void ConfigureIdentity(this IServiceCollection services)
	{
		var builder = services.AddIdentity<User, IdentityRole>(o =>
		{
			o.Password.RequireDigit = true;
			o.Password.RequireLowercase = false;
			o.Password.RequireUppercase = false;
			o.Password.RequireNonAlphanumeric = false;
			o.Password.RequiredLength = 10;
			o.User.RequireUniqueEmail = true;
		})
		.AddEntityFrameworkStores<RepositoryContext>()
		.AddDefaultTokenProviders();
	}

	public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
	{
		var jwtSettings = configuration.GetSection("JwtSettings");
		var secretKey = Environment.GetEnvironmentVariable("SECRETKEYCARMARKET");
		services.AddAuthentication(opt => {
			opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
				ValidAudience = jwtSettings.GetSection("validAudience").Value,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
			};
		});
	}

	public static void ConfigureSwagger(this IServiceCollection services)
	{
		services.AddSwaggerGen(s =>
		{
			s.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Code Maze API",
				Version = "v1",
				Description = "CompanyEmployees API by CodeMaze",
				TermsOfService = new Uri("https://example.com/terms"),
				Contact = new OpenApiContact
				{
					Name = "John Doe",
					Email = "John.Doe@gmail.com",
					Url = new Uri("https://twitter.com/johndoe"),
				},
				License = new OpenApiLicense
				{
					Name = "CompanyEmployees API LICX",
					Url = new Uri("https://example.com/license"),
				}

			});
			s.SwaggerDoc("v2", new OpenApiInfo
			{
				Title = "Code Maze API",
				Version = "v2"
			});

			var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			s.IncludeXmlComments(xmlPath);

			s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Description = "Place to add JWT with Bearer",
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer"
			});
			s.AddSecurityRequirement(new OpenApiSecurityRequirement()
			{
				{
					new OpenApiSecurityScheme {
						Reference = new OpenApiReference {
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						},
						Name = "Bearer" 
					},
					new List<string>()
				}
			});
		});
	}
}
