﻿using Entities.Models;

namespace Contracts;

public interface ICarRepository
{
	IEnumerable<Car> GetCars(Guid carShopId, bool trackChanges);
	Car? GetCar(Guid carShopId, Guid id, bool trackChanges);
	public void CreateCar(Guid carShopId, Guid brandId, Guid carcaseId, Car car);
}
