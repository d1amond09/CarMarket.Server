using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class CarShopRepository : RepositoryBase<CarShop>, ICarShopRepository
{
	public CarShopRepository(RepositoryContext repositoryContext)
	: base(repositoryContext)
	{
	}
}
