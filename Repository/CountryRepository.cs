using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class CountryRepository(RepositoryContext repositoryContext) 
	: RepositoryBase<Country>(repositoryContext), ICountryRepository
{
	public void CreateCountry(Country country) => Create(country);

	public IEnumerable<Country> GetAllCountries(bool trackChanges) =>
		[.. FindAll(trackChanges).OrderBy(c => c.Name)];

	public IEnumerable<Country> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
		[.. FindByCondition(x => ids.Contains(x.Id), trackChanges)];

	public Country? GetCountry(Guid countryId, bool trackChanges) => 
		FindByCondition(c => c.Id.Equals(countryId), trackChanges)
			.SingleOrDefault();
}
