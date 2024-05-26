using Entities.Models;

namespace Contracts;

public interface ICarcaseRepository
{
	IEnumerable<Carcase> GetAllCarcases(bool trackChanges);
	Carcase? GetCarcase(Guid id, bool trackChanges);
	public void CreateCarcase(Carcase carcase);
}
