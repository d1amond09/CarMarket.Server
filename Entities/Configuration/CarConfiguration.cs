using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
	public void Configure(EntityTypeBuilder<Car> builder)
	{
		builder.HasData
		(
			new Car
			{
				Id = new Guid("80abb123-a23d-3b20-b5de-0a570aa9764a"),
				Brand = "BMW",
				Carcase = "Sedan",
				CarShopId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
				Name = "BMW 2 seria F44",
				Price = 27999.0,
				Year = 2020,
			},
			new Car
			{
				Id = new Guid("80abb123-a23d-4b20-b5de-0a5701a9764a"),
				Brand = "BMW",
				Carcase = "SUV",
				CarShopId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
				Name = "BMW X6 F16",
				Price = 39900.0,
				Year = 2015,
			},
			new Car
			{
				Id = new Guid("81abb123-a23d-4b20-b5de-0a570aa9764a"),
				Brand = "Mercedes-Benz",
				Carcase = "Sedan",
				CarShopId = new Guid("3d490a70-94c2-4d15-9494-5248280c2ce3"),
				Name = "Mercedes-Benz E-Класс W212, S212, C207, A207",
				Price = 19999.0,
				Year = 2013,
			}
		);
	}
}
