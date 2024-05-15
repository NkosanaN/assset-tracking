using Dapper;
using Supplier.DataAccess.Interface;

namespace Supplier.DataAccess;

public class SupplierDataAccess(IDataContext dataContext) : ISupplierDataAccess
{
    private readonly IDataContext _dataContext = dataContext;
    public Task<bool> CreateSupplierAsync(Domain.Supplier supplier)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteSupplierAsync(int supplierId)
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Supplier> GetSupplierAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Domain.Supplier>> GetSuppliersAsync()
    {
        using var conn = _dataContext.CreateConnection();
        var supplierList = await conn.QueryAsync<Domain.Supplier>("");

        return supplierList;
    }
}
