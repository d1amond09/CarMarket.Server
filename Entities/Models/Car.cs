using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Car
{
	[Column("CarId")]
	public Guid Id { get; set; }

	[Required(ErrorMessage = "Car name is a required field.")]
	[MaxLength(150, ErrorMessage = "Maximum length for the Name is 150 characters.")]
	public string Name { get; set; } = "";

	[Required(ErrorMessage = "Car Price is a required field.")]
	public double Price { get; set; } = 0;

	[Required(ErrorMessage = "Car Year is a required field.")]
	[MaxLength(4, ErrorMessage = "Maximum length for the Name is 4 characters.")]
	[MinLength(4, ErrorMessage = "Minimum length for the Name is 4 characters.")]
	public string Year { get; set; } = "2000";

	[ForeignKey(nameof(Brand))]
	public Guid BrandId { get; set; }
	public Brand Brand { get; set; }

	[ForeignKey(nameof(Carcase))]
	public Guid CarcaseId { get; set; }
	public Carcase Carcase { get; set; }

	[ForeignKey(nameof(CarShop))]
	public Guid CarShopId { get; set; }
	public CarShop CarShop { get; set; }
}
