using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Country
{
	[Column("CountryId")]
	public Guid Id { get; set; }

	[Required(ErrorMessage = "Country name is a required field.")]
	[MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
	public string Name { get; set; } = "";

}
