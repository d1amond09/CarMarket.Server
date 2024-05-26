using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class AddressRepository(RepositoryContext repositoryContext) 
	: RepositoryBase<Address>(repositoryContext), IAddressRepository
{
	public void CreateAddress(Guid countryId, Address address)
	{
		address.CountryId = countryId;
		Create(address);
	}

	public Address? GetAddress(Guid countryId, Guid id, bool trackChanges) =>
		FindByCondition(a =>
		a.CountryId.Equals(countryId) && a.Id.Equals(id), trackChanges)
			.SingleOrDefault();

	public Address? GetAddress(Guid id, bool trackChanges) =>
		FindByCondition(a =>
		a.Id.Equals(id), trackChanges)
			.SingleOrDefault();

	public IEnumerable<Address> GetAddresses(Guid countryId, bool trackChanges) =>
		FindByCondition(e =>
		e.CountryId.Equals(countryId), trackChanges)
			.OrderBy(e => e.City);
}
