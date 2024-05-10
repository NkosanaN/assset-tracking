using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class SupplierRepository(DataContext context) : GenericRepository<Supplier>(context), ISupplierRepository
{
    public async Task<bool> IsSupplierNameUnique(string name)
    {
        return await _dataContext.Suppliers.AnyAsync(q => q.SupplierName == name) == false!;
    }

    public Task<IQueryable<Supplier>> GetAllSupplier()
    {
        return Task.FromResult(_dataContext.Suppliers.AsQueryable());
    }
}
