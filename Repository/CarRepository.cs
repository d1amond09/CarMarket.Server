﻿using System.ComponentModel.Design;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

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

	public async Task<IEnumerable<Car>> GetCarsAsync(Guid carShopId, bool trackChanges) =>
		await FindByCondition(e =>
		e.CarShopId.Equals(carShopId), trackChanges)
			.OrderBy(e => e.Name).ToListAsync();

}
