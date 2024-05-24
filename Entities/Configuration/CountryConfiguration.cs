using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
	public void Configure(EntityTypeBuilder<Country> builder)
	{
		builder.HasData
		(
			new Country
			{
				Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
				Name = "Belarus",
			},
			new Country
			{
				Id = new Guid("80a1bca8-6643-2b20-b5de-024705497d4a"),
				Name = "Russia",
			},
			new Country
			{
				Id = new Guid("80a1bca8-6643-2b20-b5de-024705411d4a"),
				Name = "USA",
			},
			new Country
			{
				Id = new Guid("80a1bca8-6643-2b20-b5de-024115497d4a"),
				Name = "German",
			},
			new Country
			{
				Id = new Guid("80a1bca8-6643-2220-b5de-02c115497d4a"),
				Name = "Japan",
			},
			new Country
			{
				Id = new Guid("80a1bca8-6143-2b11-b5de-024115497d4a"),
				Name = "China",
			},
			new Country
			{
				Id = new Guid("81a1bca8-6143-2b12-b4de-024111197d4b"),
				Name = "Great Britain",
			}
		);
	}
}
