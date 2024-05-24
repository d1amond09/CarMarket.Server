using Contracts;
using Entities;

namespace Repository;

public class CountryRepository : RepositoryBase<Country>, ICountryRepository
{
	public CountryRepository(RepositoryContext repositoryContext)
	: base(repositoryContext)
	{
	}
}
