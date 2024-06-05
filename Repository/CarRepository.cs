using System.ComponentModel.Design;
using System.Reflection.Metadata;
using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;

namespace Repository;

public class CarRepository(RepositoryContext repositoryContext) 
	: RepositoryBase<Car>(repositoryContext), ICarRepository
{
	public void CreateCar(Guid carShopId, Car car)
	{
		car.CarShopId = carShopId;
		Create(car);
	}

	public void DeleteCar(Car car) => Delete(car);

	public Car? GetCar(Guid carShopId, Guid id, bool trackChanges) =>
		FindByCondition(e => 
		e.CarShopId.Equals(carShopId) && e.Id.Equals(id), trackChanges)
			.SingleOrDefault();

	public IEnumerable<Car> GetCars(Guid carShopId , bool trackChanges) =>
		FindByCondition(e => 
		e.CarShopId.Equals(carShopId), trackChanges)
			.OrderBy(e => e.Name);

	public async Task<Car?> GetCarAsync(Guid carShopId, Guid id, bool trackChanges) =>
		await FindByCondition(e =>
		e.CarShopId.Equals(carShopId) && e.Id.Equals(id), trackChanges)
			.SingleOrDefaultAsync();

	public async Task<PagedList<Car>> GetCarsAsync(Guid carShopId, CarParameters carParameters, bool trackChanges)
	{
		var cars = await FindByCondition(e => e.CarShopId.Equals(carShopId), trackChanges)
				.FilterCars(carParameters.MinPrice, carParameters.MaxPrice)
				.Search(carParameters.SearchTerm)
				.OrderBy(e => e.Name)
				.Skip((carParameters.PageNumber - 1) * carParameters.PageSize)
				.Take(carParameters.PageSize)
				.ToListAsync();

		var count = await FindByCondition(e => e.CarShopId.Equals(carShopId), trackChanges).CountAsync();
		return new PagedList<Car>(cars, carParameters.PageNumber, carParameters.PageSize, count);
	}

}
