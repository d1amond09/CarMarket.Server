using Entities.Models;

namespace Contracts;

public interface ICarShopRepository
{
	IEnumerable<CarShop> GetAllCarShops(bool trackChanges);
}
