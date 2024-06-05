using Contracts;
using Entities;

namespace Repository;

public class RepositoryManager(RepositoryContext repositoryContext) : IRepositoryManager
{
	private readonly RepositoryContext _repositoryContext = repositoryContext;
	private ICarRepository? _carRepository;
	private ICarShopRepository? _carShopRepository;

	public ICarRepository Car =>
			_carRepository ??= new CarRepository(_repositoryContext);


	public ICarShopRepository CarShop =>
			_carShopRepository ??= new CarShopRepository(_repositoryContext);


	public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
}
