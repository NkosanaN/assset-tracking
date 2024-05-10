using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class ItemRepository(DataContext context) : GenericRepository<Item>(context), IItemRepository
{
    public Task<IQueryable<Item>> GetAllItem()
    {
        return Task.FromResult(_dataContext.Items.Include(c => c.ShelveBy).AsQueryable());
    }

    public async Task<Item> GetItemWithShelveById(Guid id)
    {
        return await _dataContext.Items.AsNoTracking()
            .Include(x => x.ShelveBy)
            .SingleOrDefaultAsync(c => c.ItemId == id);
    }
    public async Task<bool> IsItemNameUnique(string name)
    {
        return await _dataContext.Items.AnyAsync(q => q.Name == name) == false!;
    }
    public async Task<bool> IsItemExist(Guid Id)
    {
        bool isExist = true;

        var result =  await _dataContext.Items.FirstOrDefaultAsync(c => c.ItemId == Id);

        if (result == null)
        {
            isExist = false;
        }
      
        return isExist;
    }

    public async Task<bool> BookRepair(Guid id, string note)
    {
     
        var rowsModified = _dataContext.Database.ExecuteSql($"UPDATE [tblitem] SET DueforRepair = {true} Where [ItemId] ={id}");

        return rowsModified == 1;
    }
}

