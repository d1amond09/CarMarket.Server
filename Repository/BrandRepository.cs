using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
{
	public BrandRepository(RepositoryContext repositoryContext)
	: base(repositoryContext)
	{
	}
}
