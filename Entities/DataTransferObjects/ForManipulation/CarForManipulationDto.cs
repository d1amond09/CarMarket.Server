using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects;

public class CarForManipulationDto
{
	[Required(ErrorMessage = "Car name is a required field.")]
	[MaxLength(150, ErrorMessage = "Maximum length for the Name is 150 characters.")]
	public string Name { get; set; }

	[Required(ErrorMessage = "Car Price is a required field.")]
	[Range(0, int.MaxValue, ErrorMessage = "Price is required and it can't be lower than 0")]
	public double Price { get; set; }

	[Required(ErrorMessage = "Car Year is a required field.")]
	[MaxLength(4, ErrorMessage = "Maximum length for the Name is 4 characters.")]
	[MinLength(4, ErrorMessage = "Minimum length for the Name is 4 characters.")]
	public string Year { get; set; }

	[Required(ErrorMessage = "Brand name is a required field.")]
	[MaxLength(50, ErrorMessage = "Maximum length for the Brand is 50 characters.")]
	public string Brand { get; set; }

	[Required(ErrorMessage = "Carcase name is a required field.")]
	[MaxLength(150, ErrorMessage = "Maximum length for the Carcase is 150 characters.")]
	public string Carcase { get; set; }
}
