using Contracts;
using Entities;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
	private RepositoryContext _repositoryContext;
	private ICarRepository _carRepository;
	private ICarcaseRepository _carcaseRepository;
	private ICarShopRepository _carShopRepository;
	private ICountryRepository _countryRepository;
	private IAddressRepository _addressRepository;
	private IBrandRepository _brandRepository;
	public RepositoryManager(RepositoryContext repositoryContext)
	{
		_repositoryContext = repositoryContext;
	}
	public ICarRepository Car
	{
		get
		{
			_carRepository ??= new CarRepository(_repositoryContext);
			return _carRepository;
		}
	}
	public ICarcaseRepository Carcase
	{
		get
		{
			_carcaseRepository ??= new CarcaseRepository(_repositoryContext);
			return _carcaseRepository;
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
	public ICountryRepository Country
	{
		get
		{
			_countryRepository ??= new CountryRepository(_repositoryContext);
			return _countryRepository;
		}
	}
	public IAddressRepository Address
	{
		get
		{
			_addressRepository ??= new AddressRepository(_repositoryContext);
			return _addressRepository;
		}
	}
	public IBrandRepository Brand
	{
		get
		{
			_brandRepository ??= new BrandRepository(_repositoryContext);
			return _brandRepository;
		}
	}
	public void Save() => _repositoryContext.SaveChanges();
}
