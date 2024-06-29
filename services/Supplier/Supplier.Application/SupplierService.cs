
using Supplier.DataAccess.Interface;

namespace Supplier.Application;

public class SupplierService(ISupplierDataAccess supplierDataAccess) : ISupplierService
{
    private readonly ISupplierDataAccess _supplierDataAccess = supplierDataAccess;

    public async Task<Domain.Supplier> GetSupplierAsync()
    {
      return await  _supplierDataAccess.GetSupplierAsync();
    }

    public Task<IEnumerable<Domain.Supplier>> GetSuppliersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateSupplierAsync(Domain.Supplier supplier)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteSupplierAsync(int supplierId)
    {
        throw new NotImplementedException();
    }

}
