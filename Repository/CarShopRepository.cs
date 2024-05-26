using System.ComponentModel.Design;
using Contracts;
using Entities;
using Entities.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository;

public class CarShopRepository(RepositoryContext repositoryContext) 
	: RepositoryBase<CarShop>(repositoryContext), ICarShopRepository
{
	public void CreateCarShop(Guid addressId, CarShop carShop)
	{
		carShop.AddressId = addressId;
		Create(carShop);
	}

	public IEnumerable<CarShop> GetAllCarShops(bool trackChanges) =>
		[.. FindAll(trackChanges).OrderBy(c => c.Name)];

	public CarShop? GetCarShop(Guid carShopId, bool trackChanges) =>
		FindByCondition(c => c.Id.Equals(carShopId), trackChanges)
		.SingleOrDefault();

}
