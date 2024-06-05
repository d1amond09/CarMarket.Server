using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures;

public class CarParameters : RequestParameters
{
	public double MinPrice { get; set; }
	public double MaxPrice { get; set; } = double.MaxValue;
	public bool ValidAgeRange => MaxPrice > MinPrice;

}
