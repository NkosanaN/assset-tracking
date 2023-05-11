using Application.Contracts.Persistence;
using Domain;

namespace Persistence.Repositories;

public class DepartmentRepository : GenericRepository<Department> ,IDepartmentRepository
{
    public DepartmentRepository(DataContext context) : base(context)
    { }

    public Task<IQueryable<Department>> GetAllDepartment()
    {
        return Task.FromResult(_dataContext.Departments.AsQueryable());
    }
}

