using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Repository.Extensions.Utility;

namespace Repository.Extensions;

public static class RepositoryCarExtensions
{
	public static IQueryable<Car> FilterCars(this IQueryable<Car> cars, double minPrice, double maxPrice) =>
		cars.Where(e => (e.Price >= minPrice && e.Price <= maxPrice));

	public static IQueryable<Car> Search(this IQueryable<Car> cars, string searchTerm)
	{
		if (string.IsNullOrWhiteSpace(searchTerm))
			return cars;
		var lowerCaseTerm = searchTerm.Trim().ToLower();
		return cars.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
	}

	public static IQueryable<Car> Sort(this IQueryable<Car> cars, string orderByQueryString)
	{
		if (string.IsNullOrWhiteSpace(orderByQueryString))
			return cars.OrderBy(e => e.Name);

		var orderQuery = OrderQueryBuilder.CreateOrderQuery<Car>(orderByQueryString);

		if (string.IsNullOrWhiteSpace(orderQuery))
			return cars.OrderBy(e => e.Name);

		return cars.OrderBy(orderQuery);
	}
}

