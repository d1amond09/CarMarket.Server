using System.ComponentModel.Design;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class CarRepository : RepositoryBase<Car>, ICarRepository
{
	public CarRepository(RepositoryContext repositoryContext)
	: base(repositoryContext)
	{
	}

	public Car? GetCar(Guid carShopId, Guid id, bool trackChanges) =>
		FindByCondition(e => 
		e.CarShopId.Equals(carShopId) && e.Id.Equals(id), trackChanges)
			.SingleOrDefault();

	public IEnumerable<Car> GetCars(Guid carShopId , bool trackChanges) =>
		FindByCondition(e => 
		e.CarShopId.Equals(carShopId), trackChanges)
			.OrderBy(e => e.Name);

}
