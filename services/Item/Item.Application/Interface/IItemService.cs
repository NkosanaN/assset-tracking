namespace Item.Application.Interface;

public interface IItemService
{
    Task<IEnumerable<Domain.Item>> GetItemsAsync();
    Task<Domain.Item> GetItemAsync(Guid itemId);
    Task<bool> CreateItemAsync(Domain.Item item);
    Task<bool> DeleteItemAsync(int itemId);
}
