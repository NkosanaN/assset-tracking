using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class ItemRepository : GenericRepository<Item>, IItemRepository
{
    public ItemRepository(DataContext context) : base(context)
    { }

    public Task<IQueryable<Item>> GetAllItem()
    {
        return Task.FromResult(_dataContext.Items.AsQueryable());
    }

    public async Task<Item> GetItemWithShelveById(Guid id)
    {
        return await _dataContext.Items
            .Include(x => x.ShelveBy)
            .SingleOrDefaultAsync(c => c.ItemId == id);
    }

    public async Task<bool> IsItemNameUnique(string name)
    {
        return await _dataContext.Items.AnyAsync(q => q.Name == name) == false!;
    }
}

