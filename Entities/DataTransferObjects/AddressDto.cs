using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects;

public class AddressDto
{
	public Guid Id { get; set; }
	public string City { get; set; }
	public string Street { get; set; }
	public int House { get; set; }
}
