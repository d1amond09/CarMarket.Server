using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class BrandRepository(RepositoryContext repositoryContext) 
	: RepositoryBase<Brand>(repositoryContext), IBrandRepository
{
	public Brand? GetBrand(Guid id, bool trackChanges) =>
		FindByCondition(c => c.Id.Equals(id), trackChanges)
		.SingleOrDefault();

	public IEnumerable<Brand> GetAllBrands(bool trackChanges) =>
		[.. FindAll(trackChanges).OrderBy(c => c.Name)];

	public void CreateBrand(Brand brand) =>
		Create(brand);
}
