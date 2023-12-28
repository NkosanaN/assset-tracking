using Domain;

namespace Application.Contracts.Persistence;
public interface IItemRepository : IGenericRepository<Item>
{
    Task<IQueryable<Item>> GetAllItem();
    Task<bool> IsItemNameUnique(string name);
    Task<Item> GetItemWithShelveById(Guid id);
    Task<bool> BookRepair(Guid id, string note);
}
