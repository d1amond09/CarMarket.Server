using Entities.Models;

namespace Contracts;

public interface ICarRepository
{
	public IEnumerable<Car> GetCars(Guid carShopId, bool trackChanges);
	Car? GetCar(Guid carShopId, Guid id, bool trackChanges);
}
