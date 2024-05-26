using Entities;
using Entities.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Contracts;

public interface ICountryRepository
{
	IEnumerable<Country> GetAllCountries(bool trackChanges);
	IEnumerable<Country> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
	Country? GetCountry(Guid countryId, bool trackChanges);
	public void CreateCountry(Country country);
}
