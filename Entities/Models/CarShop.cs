using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class CarShop
{
	[Column("CarShopId")]
	public Guid Id { get; set; }

	[Required(ErrorMessage = "Car name is a required field.")]
	[MaxLength(150, ErrorMessage = "Maximum length for the Name is 150 characters.")]
	public string Name { get; set; } = "";

	[Required(ErrorMessage = "Address is a required field.")]
	[MaxLength(200, ErrorMessage = "Maximum length for the Address is 200 characters.")]
	public string Address { get; set; } = "";

	[Required(ErrorMessage = "Country is a required field.")]
	[MaxLength(100, ErrorMessage = "Maximum length for the Country is 100 characters.")]
	public string Country { get; set; } = "";

	[Required(ErrorMessage = "Phone number is a required field.")]
	[MaxLength(20, ErrorMessage = "Maximum length for the Name is 20 characters.")]
	public string Phone { get; set; } = "";

	public ICollection<Car> Cars { get; set; } = [];
}