using Entities;
using Entities.Models;

namespace Contracts;

public interface IBrandRepository
{
	IEnumerable<Brand> GetAllBrands(bool trackChanges);
	Brand? GetBrand(Guid id, bool trackChanges);
	public void CreateBrand(Brand brand);
}
