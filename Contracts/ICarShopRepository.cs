using Entities.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Contracts;

public interface ICarShopRepository
{
	IEnumerable<CarShop> GetAllCarShops(bool trackChanges);
	CarShop? GetCarShop(Guid carShopId, bool trackChanges);
	public void CreateCarShop(Guid addressId, CarShop carShop);
	public void DeleteCarShop(CarShop carShop);

}
