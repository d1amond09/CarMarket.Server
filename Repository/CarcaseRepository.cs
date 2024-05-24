using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class CarcaseRepository : RepositoryBase<Carcase>, ICarcaseRepository
{
	public CarcaseRepository(RepositoryContext repositoryContext)
	: base(repositoryContext)
	{
	}
}
