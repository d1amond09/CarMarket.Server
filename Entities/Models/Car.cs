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
	[Range(1900, int.MaxValue, ErrorMessage = "Year is required and it can't be lower than 1900")]
	public int Year { get; set; } = 2000;

	[Required(ErrorMessage = "Brand name is a required field.")]
	[MaxLength(50, ErrorMessage = "Maximum length for the Brand is 50 characters.")]
	public string Brand { get; set; }

	[Required(ErrorMessage = "Carcase name is a required field.")]
	[MaxLength(150, ErrorMessage = "Maximum length for the Carcase is 150 characters.")]
	public string Carcase { get; set; } 

	[ForeignKey(nameof(CarShop))]
	public Guid CarShopId { get; set; }
	public CarShop CarShop { get; set; }
}
