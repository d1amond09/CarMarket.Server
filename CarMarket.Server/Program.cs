using CarMarket.Server.Extensions;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.HttpOverrides;
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

		app.Run();
	}

	public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
	{
		services.ConfigureCors();
		services.ConfigureIISIntegration();
		services.ConfigureLoggerService();
		services.ConfigureSqlContext(configuration);
		services.ConfigureRepositoryManager();
		services.AddAutoMapper(x => x.CreateMap<CarShop, CarShopDto>()
				.ForMember(c => c.Name, 
				opt => opt.MapFrom(x => string.Join(' ', "Name:", x.Name))));

		services.AddControllers();
	}

	public static void ConfigureApp(WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}
		else
		{
			app.UseHsts();
		}
		app.UseHttpsRedirection();
		app.UseStaticFiles();
		app.UseCors("CorsPolicy");
		app.UseForwardedHeaders(new ForwardedHeadersOptions
		{
			ForwardedHeaders = ForwardedHeaders.All
		});
		app.UseRouting();
		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");
	}
}
