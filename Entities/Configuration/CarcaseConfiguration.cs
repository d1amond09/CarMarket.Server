using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration;

public class CarcaseConfiguration : IEntityTypeConfiguration<Carcase>
{
	public void Configure(EntityTypeBuilder<Carcase> builder)
	{
		builder.HasData
		(
			new Carcase
			{
				Id = new Guid("801b1ca8-664d-1b20-b5de-024705497d4a"),
				Name = "SUV",
			},
			new Carcase
			{
				Id = new Guid("80a1bca8-6643-2b20-b5de-024705417d4a"),
				Name = "Cabriolet",
			},
			new Carcase
			{
				Id = new Guid("80a1bca8-6643-2b20-b5de-02470541124a"),
				Name = "Coupe",
			},
			new Carcase
			{
				Id = new Guid("83a1bca8-6643-2b20-b5de-024115497d4a"),
				Name = "Passenger Van",
			},
			new Carcase
			{
				Id = new Guid("80a14ca8-6643-2220-b5da-02c115e97d4a"),
				Name = "Limousine",
			},
			new Carcase
			{
				Id = new Guid("80a1bca8-6143-2b11-b5de-024ee5497d4a"),
				Name = "Sedan",
			},
			new Carcase
			{
				Id = new Guid("a1a1bca8-6143-2b13-14de-024131ee7d4b"),
				Name = "Station Wagon",
			}
		);
	}
}
