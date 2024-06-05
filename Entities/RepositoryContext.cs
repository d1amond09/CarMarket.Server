using Entities.Configuration;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class RepositoryContext(DbContextOptions options) : DbContext(options)
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new CarShopConfiguration());
		modelBuilder.ApplyConfiguration(new CarConfiguration());
	}

	public DbSet<CarShop> CarShops { get; set; }
	public DbSet<Car> Cars { get; set; }
}
