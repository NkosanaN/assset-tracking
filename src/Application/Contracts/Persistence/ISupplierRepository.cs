namespace Application.Contracts.Persistence;

public interface ISupplierRepository : IGenericRepository<Domain.Supplier>
{
    Task<bool> IsSupplierNameUnique(string name);

    Task<IQueryable<Domain.Supplier>> GetAllSupplier();
}

