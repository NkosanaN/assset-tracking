using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DbInitializer;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceService(this IServiceCollection srv, IConfiguration config)
    {
        srv.AddDbContext<DataContext>(opt =>
            opt.UseSqlServer(config.GetConnectionString("DefaultConnection"))
        );

        srv.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        srv.AddScoped<IItemEmployeeAssignmentRepository, ItemEmployeeAssignmentRepository>();
        srv.AddScoped<IDepartmentRepository, DepartmentRepository>();
        srv.AddScoped<IItemRepository, ItemRepository>();
        srv.AddScoped<IShelveRepository, ShelveRepository>();
        srv.AddScoped<ISupplierRepository, SupplierRepository>();
        srv.AddScoped<IUserRepository, UserRepository>();
        srv.AddScoped<IDbInitializer, DbInitializer.DbInitializer>();

        return srv;
    }
}

