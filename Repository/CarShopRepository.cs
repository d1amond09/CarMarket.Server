using System.ComponentModel.Design;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository;

public class CarShopRepository(RepositoryContext repositoryContext) 
	: RepositoryBase<CarShop>(repositoryContext), ICarShopRepository
{
	public void CreateCarShop(CarShop carShop)
	{
		Create(carShop);
	}

	public void DeleteCarShop(CarShop carShop) => Delete(carShop);

	public IEnumerable<CarShop> GetAllCarShops(bool trackChanges) =>
		[.. FindAll(trackChanges).OrderBy(c => c.Name)];

	public CarShop? GetCarShop(Guid carShopId, bool trackChanges) =>
		FindByCondition(c => c.Id.Equals(carShopId), trackChanges)
		.SingleOrDefault();

	public IEnumerable<CarShop> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
		[.. FindByCondition(x => ids.Contains(x.Id), trackChanges)];

	public async Task<IEnumerable<CarShop>> GetAllCarShopsAsync(bool trackChanges) =>
		await FindAll(trackChanges)
		.OrderBy(c => c.Name)
		.ToListAsync();
	public async Task<CarShop?> GetCarShopAsync(Guid companyId, bool trackChanges) =>
		await FindByCondition(c => c.Id.Equals(companyId), trackChanges)
		.SingleOrDefaultAsync();

	public async Task<IEnumerable<CarShop>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
		await FindByCondition(x => ids.Contains(x.Id), trackChanges)
		.ToListAsync();
}
