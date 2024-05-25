using Entities.Models;

namespace Contracts;

public interface ICarShopRepository
{
	IEnumerable<CarShop> GetAllCarShops(bool trackChanges);
	CarShop? GetCarShop(Guid carShopId, bool trackChanges);
}
