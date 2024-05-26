using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Carcase
{
	[Column("CarcaseId")]
	public Guid Id { get; set; }

	[Required(ErrorMessage = "Carcase name is a required field.")]
	[MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters.")]
	public string Name { get; set; } = "";
	public ICollection<Car> Cars { get; set; }
}
