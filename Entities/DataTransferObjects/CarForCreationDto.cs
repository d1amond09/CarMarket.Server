﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects;

public class CarForCreationDto
{
	public string Name { get; set; }
	public double Price { get; set; }
	public string Year { get; set; }
}
