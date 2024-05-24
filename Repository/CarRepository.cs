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
}
