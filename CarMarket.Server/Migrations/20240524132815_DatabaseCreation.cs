using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarMarket.Server.Migrations
{
	/// <inheritdoc />
	public partial class DatabaseCreation : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
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
					Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
					House = table.Column<int>(type: "int", nullable: false)
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

			migrationBuilder.CreateTable(
				name: "CarShops",
				columns: table => new
				{
					CarShopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
					AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_CarShops", x => x.CarShopId);
					table.ForeignKey(
						name: "FK_CarShops_Addresses_AddressId",
						column: x => x.AddressId,
						principalTable: "Addresses",
						principalColumn: "AddressId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Cars",
				columns: table => new
				{
					CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
					Price = table.Column<double>(type: "float", nullable: false),
					Year = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
					BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CarcaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CarShopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Cars", x => x.CarId);
					table.ForeignKey(
						name: "FK_Cars_Brands_BrandId",
						column: x => x.BrandId,
						principalTable: "Brands",
						principalColumn: "BrandId",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Cars_CarShops_CarShopId",
						column: x => x.CarShopId,
						principalTable: "CarShops",
						principalColumn: "CarShopId",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Cars_Carcases_CarcaseId",
						column: x => x.CarcaseId,
						principalTable: "Carcases",
						principalColumn: "CarcaseId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Addresses_CountryId",
				table: "Addresses",
				column: "CountryId");

			migrationBuilder.CreateIndex(
				name: "IX_Cars_BrandId",
				table: "Cars",
				column: "BrandId");

			migrationBuilder.CreateIndex(
				name: "IX_Cars_CarcaseId",
				table: "Cars",
				column: "CarcaseId");

			migrationBuilder.CreateIndex(
				name: "IX_Cars_CarShopId",
				table: "Cars",
				column: "CarShopId");

			migrationBuilder.CreateIndex(
				name: "IX_CarShops_AddressId",
				table: "CarShops",
				column: "AddressId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Cars");

			migrationBuilder.DropTable(
				name: "Brands");

			migrationBuilder.DropTable(
				name: "CarShops");

			migrationBuilder.DropTable(
				name: "Carcases");

			migrationBuilder.DropTable(
				name: "Addresses");

			migrationBuilder.DropTable(
				name: "Countries");
		}
	}
}
