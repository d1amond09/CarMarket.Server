﻿using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration;

public class CarShopConfiguration : IEntityTypeConfiguration<CarShop>
{
	public void Configure(EntityTypeBuilder<CarShop> builder)
	{
		builder.HasData
		(
			new CarShop
			{
				Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
				Name = "AutoShop Ltd",
				Phone = "+375296578123",
				Address = "Gomel, Sovetskaia 15",
				Country = "Belarus"
			},
			new CarShop
			{
				Id = new Guid("3d490a70-94c2-4d15-9494-5248280c2ce3"),
				Name = "CarShop Ltd",
				Phone = "+49446578123",
				Address = "Berlin, Pohlstrbe 12",
				Country = "German",
			}	
		);
	}
}
