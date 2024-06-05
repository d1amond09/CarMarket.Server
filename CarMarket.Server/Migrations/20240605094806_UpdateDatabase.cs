using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarMarket.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Carcases_CarcaseId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_CarShops_Addresses_AddressId",
                table: "CarShops");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Carcases");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_CarShops_AddressId",
                table: "CarShops");

            migrationBuilder.DropIndex(
                name: "IX_Cars_BrandId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarcaseId",
                table: "Cars");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: new Guid("80abb123-a23d-4120-b5de-0a570aa9764a"));

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "CarShops");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarcaseId",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "CarShops",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "CarShops",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Cars",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Carcase",
                table: "Cars",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "CarShops",
                keyColumn: "CarShopId",
                keyValue: new Guid("3d490a70-94c2-4d15-9494-5248280c2ce3"),
                columns: new[] { "Address", "Country" },
                values: new object[] { "Berlin, Pohlstrbe 12", "German" });

            migrationBuilder.UpdateData(
                table: "CarShops",
                keyColumn: "CarShopId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "Address", "Country" },
                values: new object[] { "Gomel, Sovetskaia 15", "Belarus" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: new Guid("80abb123-a23d-3b20-b5de-0a570aa9764a"),
                columns: new[] { "Brand", "Carcase", "Year" },
                values: new object[] { "BMW", "Sedan", 2020 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: new Guid("80abb123-a23d-4b20-b5de-0a5701a9764a"),
                columns: new[] { "Brand", "Carcase", "Year" },
                values: new object[] { "BMW", "SUV", 2015 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: new Guid("81abb123-a23d-4b20-b5de-0a570aa9764a"),
                columns: new[] { "Brand", "Carcase", "Year" },
                values: new object[] { "Mercedes-Benz", "Sedan", 2013 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "CarShops");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "CarShops");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Carcase",
                table: "Cars");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "CarShops",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "Cars",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "BrandId",
                table: "Cars",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CarcaseId",
                table: "Cars",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Carcases",
                columns: table => new
                {
                    CarcaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carcases", x => x.CarcaseId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    House = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.UpdateData(
                table: "CarShops",
                keyColumn: "CarShopId",
                keyValue: new Guid("3d490a70-94c2-4d15-9494-5248280c2ce3"),
                column: "AddressId",
                value: new Guid("80abb1a8-a64d-4b20-b5de-0a470aa9764a"));

            migrationBuilder.UpdateData(
                table: "CarShops",
                keyColumn: "CarShopId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                column: "AddressId",
                value: new Guid("10a1bca8-66a3-2b20-b5de-0a4a05497d4a"));

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

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: new Guid("80abb123-a23d-3b20-b5de-0a570aa9764a"),
                columns: new[] { "BrandId", "CarcaseId", "Year" },
                values: new object[] { new Guid("80abb1a8-a64d-4b20-b5de-0a470aa97d4a"), new Guid("80a1bca8-6143-2b11-b5de-024ee5497d4a"), "2020" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: new Guid("80abb123-a23d-4b20-b5de-0a5701a9764a"),
                columns: new[] { "BrandId", "CarcaseId", "Year" },
                values: new object[] { new Guid("80abb1a8-a64d-4b20-b5de-0a470aa97d4a"), new Guid("801b1ca8-664d-1b20-b5de-024705497d4a"), "2015" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: new Guid("81abb123-a23d-4b20-b5de-0a570aa9764a"),
                columns: new[] { "BrandId", "CarcaseId", "Year" },
                values: new object[] { new Guid("80a1bca8-6143-2b11-b5de-024115417d4a"), new Guid("80a1bca8-6643-2b20-b5de-02470541124a"), "2013" });

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
                table: "Cars",
                columns: new[] { "CarId", "BrandId", "CarShopId", "CarcaseId", "Name", "Price", "Year" },
                values: new object[] { new Guid("80abb123-a23d-4120-b5de-0a570aa9764a"), new Guid("80a1bca8-6143-2b11-b5de-024115417d4a"), new Guid("3d490a70-94c2-4d15-9494-5248280c2ce3"), new Guid("a1a1bca8-6143-2b13-14de-024131ee7d4b"), "Mercedes-Benz CLS C218, X218", 27400.0, "2015" });

            migrationBuilder.CreateIndex(
                name: "IX_CarShops_AddressId",
                table: "CarShops",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BrandId",
                table: "Cars",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarcaseId",
                table: "Cars",
                column: "CarcaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Carcases_CarcaseId",
                table: "Cars",
                column: "CarcaseId",
                principalTable: "Carcases",
                principalColumn: "CarcaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarShops_Addresses_AddressId",
                table: "CarShops",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
