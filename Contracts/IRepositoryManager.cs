namespace Contracts;

public interface IRepositoryManager
{
	ICarcaseRepository Carcase { get; }
	ICarRepository Car { get; }
	ICarShopRepository CarShop { get; }
	ICountryRepository Country { get; }
	IBrandRepository Brand { get; }
	IAddressRepository Address { get; }
	void Save();
}

