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
	public double Price { get; set; } = 0;

	[Required(ErrorMessage = "Car Year is a required field.")]
	[Range(1800, 9999, ErrorMessage = "Price is required and it can't be lower than 1900")]
	public int Year { get; set; } = 2000;

	[Required(ErrorMessage = "Brand name is a required field.")]
	[MaxLength(50, ErrorMessage = "Maximum length for the Brand is 50 characters.")]
	public string Brand { get; set; } = string.Empty;

	[Required(ErrorMessage = "Carcase name is a required field.")]
	[MaxLength(150, ErrorMessage = "Maximum length for the Carcase is 150 characters.")]
	public string Carcase { get; set; }
}
