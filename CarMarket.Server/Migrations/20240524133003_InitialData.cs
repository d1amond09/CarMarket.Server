using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarMarket.Server.Migrations
{
	/// <inheritdoc />
	public partial class InitialData : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.InsertData(
				table: "Brands",
				columns: new[] { "BrandId", "Name" },
				values: new object[,]
				{
					{ new Guid("80a1bca8-6143-2b11-b5de-024115417d4a"), "Mercedes-Benz" },
					{ new Guid("80a1bca8-6643-2220-b5de-02c115497a4a"), "Porsche" },
					{ new Guid("80a1bca8-6643-2b20-b5de-02411aa97d4a"), "Volkswagen" },
					{ new Guid("80abb1a8-a64d-4b20-b5de-0a470aa97d4a"), "BMW" },
					{ new Guid("81a1bca8-6143-2b12-b4de-022111197d4b"), "Audi" },
					{ new Guid("81a1bca8-6143-2b12-b4de-0221aa197aab"), "Haval" },
					{ new Guid("82a1bca8-6143-2332-b4de-0221aa197aab"), "Toyota" },
					{ new Guid("8aa1eca8-5643-2b20-b5de-0a7705411d4a"), "Jaguar" },
					{ new Guid("a0a1bca8-66a3-2b20-b5de-0a4a05497d4a"), "МАЗ" }
				});

			migrationBuilder.InsertData(
				table: "Carcases",
				columns: new[] { "CarcaseId", "Name" },
				values: new object[,]
				{
					{ new Guid("801b1ca8-664d-1b20-b5de-024705497d4a"), "SUV" },
					{ new Guid("80a14ca8-6643-2220-b5da-02c115e97d4a"), "Limousine" },
					{ new Guid("80a1bca8-6143-2b11-b5de-024ee5497d4a"), "Sedan" },
					{ new Guid("80a1bca8-6643-2b20-b5de-02470541124a"), "Coupe" },
					{ new Guid("80a1bca8-6643-2b20-b5de-024705417d4a"), "Cabriolet" },
					{ new Guid("83a1bca8-6643-2b20-b5de-024115497d4a"), "Passenger Van" },
					{ new Guid("a1a1bca8-6143-2b13-14de-024131ee7d4b"), "Station Wagon" }
				});

			migrationBuilder.InsertData(
				table: "Countries",
				columns: new[] { "CountryId", "Name" },
				values: new object[,]
				{
					{ new Guid("80a1bca8-6143-2b11-b5de-024115497d4a"), "China" },
					{ new Guid("80a1bca8-6643-2220-b5de-02c115497d4a"), "Japan" },
					{ new Guid("80a1bca8-6643-2b20-b5de-024115497d4a"), "German" },
					{ new Guid("80a1bca8-6643-2b20-b5de-024705411d4a"), "USA" },
					{ new Guid("80a1bca8-6643-2b20-b5de-024705497d4a"), "Russia" },
					{ new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Belarus" },
					{ new Guid("81a1bca8-6143-2b12-b4de-024111197d4b"), "Great Britain" }
				});

			migrationBuilder.InsertData(
				table: "Addresses",
				columns: new[] { "AddressId", "City", "CountryId", "House", "Street" },
				values: new object[,]
				{
					{ new Guid("10a1bca8-66a3-2b20-b5de-0a4a05497d4a"), "Minsk", new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 12, "Советская" },
					{ new Guid("20a1bca8-6643-2b20-b5de-02411aa97d4a"), "Munich", new Guid("80a1bca8-6643-2b20-b5de-024115497d4a"), 21, "Alter-Hof-Str" },
					{ new Guid("7aa1eca8-5643-2b20-b5de-0a7705411d4a"), "London", new Guid("81a1bca8-6143-2b12-b4de-024111197d4b"), 9, "John-Adam-Str" },
					{ new Guid("80abb1a8-a64d-4b20-b5de-0a470aa9764a"), "Berlin", new Guid("80a1bca8-6643-2b20-b5de-024115497d4a"), 2, "Karl-Liebknecht-Str" }
				});

			migrationBuilder.InsertData(
				table: "CarShops",
				columns: new[] { "CarShopId", "AddressId", "Name", "Phone" },
				values: new object[,]
				{
					{ new Guid("3d490a70-94c2-4d15-9494-5248280c2ce3"), new Guid("80abb1a8-a64d-4b20-b5de-0a470aa9764a"), "CarShop Ltd", "+49446578123" },
					{ new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Guid("10a1bca8-66a3-2b20-b5de-0a4a05497d4a"), "AutoShop Ltd", "+375296578123" }
				});

			migrationBuilder.InsertData(
				table: "Cars",
				columns: new[] { "CarId", "BrandId", "CarShopId", "CarcaseId", "Name", "Price", "Year" },
				values: new object[,]
				{
					{ new Guid("80abb123-a23d-3b20-b5de-0a570aa9764a"), new Guid("80abb1a8-a64d-4b20-b5de-0a470aa97d4a"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Guid("80a1bca8-6143-2b11-b5de-024ee5497d4a"), "BMW 2 seria F44", 27999.0, "2020" },
					{ new Guid("80abb123-a23d-4120-b5de-0a570aa9764a"), new Guid("80a1bca8-6143-2b11-b5de-024115417d4a"), new Guid("3d490a70-94c2-4d15-9494-5248280c2ce3"), new Guid("a1a1bca8-6143-2b13-14de-024131ee7d4b"), "Mercedes-Benz CLS C218, X218", 27400.0, "2015" },
					{ new Guid("80abb123-a23d-4b20-b5de-0a5701a9764a"), new Guid("80abb1a8-a64d-4b20-b5de-0a470aa97d4a"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Guid("801b1ca8-664d-1b20-b5de-024705497d4a"), "BMW X6 F16", 39900.0, "2015" },
					{ new Guid("81abb123-a23d-4b20-b5de-0a570aa9764a"), new Guid("80a1bca8-6143-2b11-b5de-024115417d4a"), new Guid("3d490a70-94c2-4d15-9494-5248280c2ce3"), new Guid("80a1bca8-6643-2b20-b5de-02470541124a"), "Mercedes-Benz E-Класс W212, S212, C207, A207", 19999.0, "2013" }
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "Addresses",
				keyColumn: "AddressId",
				keyValue: new Guid("20a1bca8-6643-2b20-b5de-02411aa97d4a"));

			migrationBuilder.DeleteData(
				table: "Addresses",
				keyColumn: "AddressId",
				keyValue: new Guid("7aa1eca8-5643-2b20-b5de-0a7705411d4a"));

			migrationBuilder.DeleteData(
				table: "Brands",
				keyColumn: "BrandId",
				keyValue: new Guid("80a1bca8-6643-2220-b5de-02c115497a4a"));

			migrationBuilder.DeleteData(
				table: "Brands",
				keyColumn: "BrandId",
				keyValue: new Guid("80a1bca8-6643-2b20-b5de-02411aa97d4a"));

			migrationBuilder.DeleteData(
				table: "Brands",
				keyColumn: "BrandId",
				keyValue: new Guid("81a1bca8-6143-2b12-b4de-022111197d4b"));

			migrationBuilder.DeleteData(
				table: "Brands",
				keyColumn: "BrandId",
				keyValue: new Guid("81a1bca8-6143-2b12-b4de-0221aa197aab"));

			migrationBuilder.DeleteData(
				table: "Brands",
				keyColumn: "BrandId",
				keyValue: new Guid("82a1bca8-6143-2332-b4de-0221aa197aab"));

			migrationBuilder.DeleteData(
				table: "Brands",
				keyColumn: "BrandId",
				keyValue: new Guid("8aa1eca8-5643-2b20-b5de-0a7705411d4a"));

			migrationBuilder.DeleteData(
				table: "Brands",
				keyColumn: "BrandId",
				keyValue: new Guid("a0a1bca8-66a3-2b20-b5de-0a4a05497d4a"));

			migrationBuilder.DeleteData(
				table: "Carcases",
				keyColumn: "CarcaseId",
				keyValue: new Guid("80a14ca8-6643-2220-b5da-02c115e97d4a"));

			migrationBuilder.DeleteData(
				table: "Carcases",
				keyColumn: "CarcaseId",
				keyValue: new Guid("80a1bca8-6643-2b20-b5de-024705417d4a"));

			migrationBuilder.DeleteData(
				table: "Carcases",
				keyColumn: "CarcaseId",
				keyValue: new Guid("83a1bca8-6643-2b20-b5de-024115497d4a"));

			migrationBuilder.DeleteData(
				table: "Cars",
				keyColumn: "CarId",
				keyValue: new Guid("80abb123-a23d-3b20-b5de-0a570aa9764a"));

			migrationBuilder.DeleteData(
				table: "Cars",
				keyColumn: "CarId",
				keyValue: new Guid("80abb123-a23d-4120-b5de-0a570aa9764a"));

			migrationBuilder.DeleteData(
				table: "Cars",
				keyColumn: "CarId",
				keyValue: new Guid("80abb123-a23d-4b20-b5de-0a5701a9764a"));

			migrationBuilder.DeleteData(
				table: "Cars",
				keyColumn: "CarId",
				keyValue: new Guid("81abb123-a23d-4b20-b5de-0a570aa9764a"));

			migrationBuilder.DeleteData(
				table: "Countries",
				keyColumn: "CountryId",
				keyValue: new Guid("80a1bca8-6143-2b11-b5de-024115497d4a"));

			migrationBuilder.DeleteData(
				table: "Countries",
				keyColumn: "CountryId",
				keyValue: new Guid("80a1bca8-6643-2220-b5de-02c115497d4a"));

			migrationBuilder.DeleteData(
				table: "Countries",
				keyColumn: "CountryId",
				keyValue: new Guid("80a1bca8-6643-2b20-b5de-024705411d4a"));

			migrationBuilder.DeleteData(
				table: "Countries",
				keyColumn: "CountryId",
				keyValue: new Guid("80a1bca8-6643-2b20-b5de-024705497d4a"));

			migrationBuilder.DeleteData(
				table: "Brands",
				keyColumn: "BrandId",
				keyValue: new Guid("80a1bca8-6143-2b11-b5de-024115417d4a"));

			migrationBuilder.DeleteData(
				table: "Brands",
				keyColumn: "BrandId",
				keyValue: new Guid("80abb1a8-a64d-4b20-b5de-0a470aa97d4a"));

			migrationBuilder.DeleteData(
				table: "CarShops",
				keyColumn: "CarShopId",
				keyValue: new Guid("3d490a70-94c2-4d15-9494-5248280c2ce3"));

			migrationBuilder.DeleteData(
				table: "CarShops",
				keyColumn: "CarShopId",
				keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

			migrationBuilder.DeleteData(
				table: "Carcases",
				keyColumn: "CarcaseId",
				keyValue: new Guid("801b1ca8-664d-1b20-b5de-024705497d4a"));

			migrationBuilder.DeleteData(
				table: "Carcases",
				keyColumn: "CarcaseId",
				keyValue: new Guid("80a1bca8-6143-2b11-b5de-024ee5497d4a"));

			migrationBuilder.DeleteData(
				table: "Carcases",
				keyColumn: "CarcaseId",
				keyValue: new Guid("80a1bca8-6643-2b20-b5de-02470541124a"));

			migrationBuilder.DeleteData(
				table: "Carcases",
				keyColumn: "CarcaseId",
				keyValue: new Guid("a1a1bca8-6143-2b13-14de-024131ee7d4b"));

			migrationBuilder.DeleteData(
				table: "Countries",
				keyColumn: "CountryId",
				keyValue: new Guid("81a1bca8-6143-2b12-b4de-024111197d4b"));

			migrationBuilder.DeleteData(
				table: "Addresses",
				keyColumn: "AddressId",
				keyValue: new Guid("10a1bca8-66a3-2b20-b5de-0a4a05497d4a"));

			migrationBuilder.DeleteData(
				table: "Addresses",
				keyColumn: "AddressId",
				keyValue: new Guid("80abb1a8-a64d-4b20-b5de-0a470aa9764a"));

			migrationBuilder.DeleteData(
				table: "Countries",
				keyColumn: "CountryId",
				keyValue: new Guid("80a1bca8-6643-2b20-b5de-024115497d4a"));

			migrationBuilder.DeleteData(
				table: "Countries",
				keyColumn: "CountryId",
				keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));
		}
	}
}
