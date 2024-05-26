﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Entities.DataTransferObjects;

public class CountryForUpdateDto
{
	public string Name { get; set; }
	public ICollection<AddressForUpdateDto> Addresses { get; set; }
}
