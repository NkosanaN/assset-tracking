namespace Application.Contracts.Persistence;

public interface ISupplierRepository
{
	Task<bool> IsSupplierNameUnique(string name);

	Task<IQueryable<Domain.Supplier>> GetAllSupplier();
}

