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

	[ForeignKey(nameof(Address))]
	public Guid AddressId { get; set; }
	public Address Address { get; set; }

	[Required(ErrorMessage = "Phone number is a required field.")]
	[MaxLength(20, ErrorMessage = "Maximum length for the Name is 20 characters.")]
	public string Phone { get; set; } = "";

	public ICollection<Car> Cars { get; set; }
}