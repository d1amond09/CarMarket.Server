using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class CarcaseRepository(RepositoryContext repositoryContext) 
	: RepositoryBase<Carcase>(repositoryContext), ICarcaseRepository
{
	public Carcase? GetCarcase(Guid id, bool trackChanges) =>
		FindByCondition(c => c.Id.Equals(id), trackChanges)
		.SingleOrDefault();

	public IEnumerable<Carcase> GetAllCarcases(bool trackChanges) =>
		[.. FindAll(trackChanges).OrderBy(c => c.Name)];

	public void CreateCarcase(Carcase carcase) =>
		Create(carcase);
}
