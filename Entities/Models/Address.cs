using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Address
{
	[Column("AddressId")]
	public Guid Id { get; set; }

	[ForeignKey(nameof(Country))]
	public Guid CountryId { get; set; }
	public Country Country { get; set; }

	[Required(ErrorMessage = "City is a required field.")]
	[MaxLength(200, ErrorMessage = "Maximum length for the City is 200 characters.")]
	public string City { get; set; } = "";

	[Required(ErrorMessage = "Street is a required field.")]
	[MaxLength(200, ErrorMessage = "Maximum length for the Street is 200 characters.")]
	public string Street { get; set; } = "";

	[Required(ErrorMessage = "House is a required field.")]
	public int House { get; set; } = 0;
}
