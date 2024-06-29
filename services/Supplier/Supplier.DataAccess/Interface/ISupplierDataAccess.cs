namespace Supplier.DataAccess.Interface;

public interface ISupplierDataAccess
{
    Task<IEnumerable<Domain.Supplier>> GetSuppliersAsync();
    Task<Domain.Supplier> GetSupplierAsync();
    Task<bool> CreateSupplierAsync(Domain.Supplier supplier);
    Task<bool> DeleteSupplierAsync(int supplierId);
}
