using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects;

public class CarDto
{
	public Guid Id { get; set; }
	public string Name { get; set; } = "";
	public double Price { get; set; } = 0;
	public int Year { get; set; } = 0;
	public string Brand { get; set; } = "";
	public string Carcase { get; set; } = "";
}
