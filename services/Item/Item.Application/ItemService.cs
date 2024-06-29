using Item.Application.Interface;
using Item.DataAccess;
using Item.DataAccess.Interface;

namespace Item.Application;

//public class ItemService(IItemDataAccess itemDataAccess) : IItemService
//{
public class ItemService : IItemService
{
    private readonly IItemDataAccess _itemDataAccess ;
    public ItemService(IItemDataAccess itemDataAccess) 
    {
        _itemDataAccess = itemDataAccess;
    }


    public async Task<IEnumerable<Domain.Item>> GetItemsAsync()
    {
        var item = await _itemDataAccess.GetItemsAsync();

        return item!;
    }

    public async Task<Domain.Item> GetItemAsync(Guid itemId)
    {
        //var item = await _itemDataAccess.GetItemAsync(itemId);

        //return item!;
        throw new NotImplementedException();
    }

    public async Task<bool> CreateItemAsync(Domain.Item item)
    {
        //bool state = false;
        //var result = await _itemDataAccess.CreateItemAsync(item);

        //if (result is true)
        //    state = true;

        //return state;
        throw new NotImplementedException();
    }

    public Task<bool> DeleteItemAsync(int itemId)
    {
        throw new NotImplementedException();
    }

}
