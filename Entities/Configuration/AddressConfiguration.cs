using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
	public void Configure(EntityTypeBuilder<Address> builder)
	{
		builder.HasData
		(
			new Address
			{
				Id = new Guid("80abb1a8-a64d-4b20-b5de-0a470aa9764a"),
				CountryId = new Guid("80a1bca8-6643-2b20-b5de-024115497d4a"),
				City = "Berlin",
				Street = "Karl-Liebknecht-Str",
				House = 2,
			},
			new Address
			{
				Id = new Guid("10a1bca8-66a3-2b20-b5de-0a4a05497d4a"),
				CountryId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
				City = "Minsk",
				Street = "Советская",
				House = 12,
			},
			new Address
			{
				Id = new Guid("7aa1eca8-5643-2b20-b5de-0a7705411d4a"),
				CountryId = new Guid("81a1bca8-6143-2b12-b4de-024111197d4b"),
				City = "London",
				Street = "John-Adam-Str",
				House = 9,
			},
			new Address
			{
				Id = new Guid("20a1bca8-6643-2b20-b5de-02411aa97d4a"),
				CountryId = new Guid("80a1bca8-6643-2b20-b5de-024115497d4a"),
				City = "Munich",
				Street = "Alter-Hof-Str",
				House = 21,
			}
		);
	}
}
