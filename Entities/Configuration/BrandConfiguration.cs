using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
	public void Configure(EntityTypeBuilder<Brand> builder)
	{
		builder.HasData
		(
			new Brand
			{
				Id = new Guid("80abb1a8-a64d-4b20-b5de-0a470aa97d4a"),
				Name = "BMW",
			},
			new Brand
			{
				Id = new Guid("a0a1bca8-66a3-2b20-b5de-0a4a05497d4a"),
				Name = "МАЗ",
			},
			new Brand
			{
				Id = new Guid("8aa1eca8-5643-2b20-b5de-0a7705411d4a"),
				Name = "Jaguar",
			},
			new Brand
			{
				Id = new Guid("80a1bca8-6643-2b20-b5de-02411aa97d4a"),
				Name = "Volkswagen",
			},
			new Brand
			{
				Id = new Guid("80a1bca8-6643-2220-b5de-02c115497a4a"),
				Name = "Porsche",
			},
			new Brand
			{
				Id = new Guid("80a1bca8-6143-2b11-b5de-024115417d4a"),
				Name = "Mercedes-Benz",
			},
			new Brand
			{
				Id = new Guid("81a1bca8-6143-2b12-b4de-022111197d4b"),
				Name = "Audi",
			},
			new Brand
			{
				Id = new Guid("81a1bca8-6143-2b12-b4de-0221aa197aab"),
				Name = "Haval",
			},
			new Brand
			{
				Id = new Guid("82a1bca8-6143-2332-b4de-0221aa197aab"),
				Name = "Toyota",
			}
		);
	}
}
