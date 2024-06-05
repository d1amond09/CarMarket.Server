using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts;

public interface ICarRepository
{
	IEnumerable<Car> GetCars(Guid carShopId, bool trackChanges);
	Car? GetCar(Guid carShopId, Guid id, bool trackChanges);
	Task<PagedList<Car>> GetCarsAsync(Guid carShopId, CarParameters carParameters, bool trackChanges);
	Task<Car?> GetCarAsync(Guid carShopId, Guid id, bool trackChanges);
	public void CreateCar(Guid carShopId, Car car);
	public void DeleteCar(Car car);
}
