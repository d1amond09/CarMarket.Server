﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Entities.DataTransferObjects;

public class BrandDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
}