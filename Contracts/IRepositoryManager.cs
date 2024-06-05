namespace Contracts;

public interface IRepositoryManager
{
	ICarRepository Car { get; }
	ICarShopRepository CarShop { get; }
	void Save();
}

