using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class ShelveRepository(DataContext context) : GenericRepository<ShelveType>(context) , IShelveRepository
{
    public Task<IQueryable<ShelveType>> GetAllShelveType()
    {
        return Task.FromResult(_dataContext.ShelveTypes.AsQueryable());
    }

    public async Task<bool> IsShelveTypeUnique(string tag)
    {
        return await _dataContext.ShelveTypes.AnyAsync(q => q.ShelfTag == tag) == false!;
    }
}

