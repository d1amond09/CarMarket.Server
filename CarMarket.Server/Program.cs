using NLog;
using CarMarket.Server.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;

namespace CarMarket.Server;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		LogManager.Setup().LoadConfigurationFromFile("nlog.config", true);

		ConfigureServices(builder.Services);

		var app = builder.Build();

		ConfigureApp(app);

		app.Run();
	}

	public static void ConfigureServices(IServiceCollection services)
	{
		services.ConfigureCors();
		services.ConfigureIISIntegration();
		services.ConfigureLoggerService();

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
