using Contracts;
using Entities;
using Entities.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository;

public class CarShopRepository : RepositoryBase<CarShop>, ICarShopRepository
{
	public CarShopRepository(RepositoryContext repositoryContext)
	: base(repositoryContext)
	{

	}

	public IEnumerable<CarShop> GetAllCarShops(bool trackChanges) =>
		[.. FindAll(trackChanges).OrderBy(c => c.Name)];
}
