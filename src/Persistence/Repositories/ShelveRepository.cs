using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class ShelveRepository : GenericRepository<ShelveType> , IShelveRepository
{
    public ShelveRepository(DataContext context) : base(context)
    { }

    public Task<IQueryable<ShelveType>> GetAllShelveType()
    {
        return Task.FromResult(_dataContext.ShelveTypes.AsQueryable());
    }

    public async Task<bool> IsShelveTypeUnique(string tag)
    {
        return await _dataContext.ShelveTypes.AnyAsync(q => q.ShelfTag == tag) == false!;
    }
}

