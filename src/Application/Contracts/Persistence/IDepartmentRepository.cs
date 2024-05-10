using Domain;

namespace Application.Contracts.Persistence;

public interface IDepartmentRepository : IGenericRepository<Department>
{
    Task<IQueryable<Department>> GetAllDepartment();
}

