using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class AddressRepository : RepositoryBase<Address>, IAddressRepository
{
	public AddressRepository(RepositoryContext repositoryContext)
	: base(repositoryContext)
	{
	}
}
