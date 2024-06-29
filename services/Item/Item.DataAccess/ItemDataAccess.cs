using Item.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace Item.DataAccess;

public class ItemDataAccess: IItemDataAccess
{
    private readonly DataContext _dataAccess;
    public ItemDataAccess(DataContext dataAccess)
    {
        _dataAccess = dataAccess;
    }

    //public class ItemDataAccess(DataContext dataAccess) : IItemDataAccess
    //{



    public async Task<IEnumerable<Domain.Item>> GetItemsAsync()
    {
        var items = await _dataAccess.Items.ToListAsync();

        return items;
    }

    public async Task<Domain.Item> GetItemAsync(Guid itemId)
    {
        var item = await _dataAccess.Items.Where(x=>x.ItemId == itemId).FirstOrDefaultAsync();

        return item!;
    }

    public async Task<bool> CreateItemAsync(Domain.Item item)
    {
        bool state = false;

       //await _dataAccess.AddAsync(item);

       // var result  = await _dataAccess.SaveChangesAsync() > 1;

       // if(result)
       //     state = true;

        return state;
    }

    public async Task<bool> DeleteItemAsync(int itemId)
    {
        throw new NotImplementedException();
    }
}
