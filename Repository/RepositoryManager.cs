using Contracts;
using Entities;

namespace Repository;

public class RepositoryManager(RepositoryContext repositoryContext) : IRepositoryManager
{
	private readonly RepositoryContext _repositoryContext = repositoryContext;
	private ICarRepository _carRepository;
	private ICarShopRepository _carShopRepository;

	public ICarRepository Car
	{
		get
		{
			_carRepository ??= new CarRepository(_repositoryContext);
			return _carRepository;
		}
	}

	public ICarShopRepository CarShop
	{
		get
		{
			_carShopRepository ??= new CarShopRepository(_repositoryContext);
			return _carShopRepository;
		}
	}

	public void Save() => _repositoryContext.SaveChanges();
}
