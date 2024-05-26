using Entities.Models;

namespace Contracts;

public interface IAddressRepository
{
	IEnumerable<Address> GetAddresses(Guid countryId, bool trackChanges);
	Address? GetAddress(Guid countryId, Guid id, bool trackChanges);
	Address? GetAddress(Guid id, bool trackChanges);
	public void CreateAddress(Guid countryId, Address address);
}
