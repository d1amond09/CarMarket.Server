using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects;

public class AddressForUpdateDto
{
	public string City { get; set; }
	public string Street { get; set; }
	public int House { get; set; }
}
