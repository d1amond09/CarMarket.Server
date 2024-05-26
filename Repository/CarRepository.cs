using System.ComponentModel.Design;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class CarRepository(RepositoryContext repositoryContext) 
	: RepositoryBase<Car>(repositoryContext), ICarRepository
{
	public void CreateCar(Guid carShopId, Guid brandId, Guid carcaseId, Car car)
	{
		car.CarShopId = carShopId;
		car.BrandId = brandId;
		car.CarcaseId = carcaseId;
		Create(car);
	}

	public void DeleteCar(Car car) => Delete(car);

	public Car? GetCar(Guid carShopId, Guid id, bool trackChanges) =>
		FindByCondition(e => 
		e.CarShopId.Equals(carShopId) && e.Id.Equals(id), trackChanges)
			.SingleOrDefault();

	public IEnumerable<Car> GetCars(Guid carShopId , bool trackChanges) =>
		FindByCondition(e => 
		e.CarShopId.Equals(carShopId), trackChanges)
			.OrderBy(e => e.Name);

}
