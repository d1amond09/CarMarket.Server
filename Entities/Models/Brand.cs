using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Brand
{

	[Column("BrandId")]
	public Guid Id { get; set; }

	[Required(ErrorMessage = "Brand name is a required field.")]
	[MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters.")]
	public string Name { get; set; } = "";

}
