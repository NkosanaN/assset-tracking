using Domain;

namespace Application.Contracts.Persistence;

public interface IShelveRepository : IGenericRepository<ShelveType>
{
	Task<IQueryable<ShelveType>> GetAllShelveType();
	Task<bool> IsShelveTypeUnique(string tag);
}

