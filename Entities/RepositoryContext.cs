using Entities.Configuration;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class RepositoryContext : DbContext
{
	public RepositoryContext(DbContextOptions options)
	: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new CarcaseConfiguration());
		modelBuilder.ApplyConfiguration(new CountryConfiguration());
		modelBuilder.ApplyConfiguration(new AddressConfiguration());
		modelBuilder.ApplyConfiguration(new CarShopConfiguration());
		modelBuilder.ApplyConfiguration(new BrandConfiguration());
		modelBuilder.ApplyConfiguration(new CarConfiguration());
	}

	public DbSet<CarShop> CarShops { get; set; }
	public DbSet<Car> Cars { get; set; }
	public DbSet<Brand> Brands { get; set; }
	public DbSet<Carcase> Carcases { get; set; }
	public DbSet<Address> Addresses { get; set; }
	public DbSet<Country> Countries { get; set; }
}
