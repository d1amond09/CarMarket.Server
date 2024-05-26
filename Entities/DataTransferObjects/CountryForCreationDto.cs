using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Entities.DataTransferObjects;

public class CountryForCreationDto
{
	public string Name { get; set; }
	public ICollection<AddressForCreationDto> Addresses { get; set; }
}
