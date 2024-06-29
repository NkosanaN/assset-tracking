using Microsoft.Extensions.DependencyInjection;
using Supplier.DataAccess.Interface;

namespace Supplier.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccessService(this IServiceCollection services) 
    {
        services.AddSingleton<IDataContext, DataContext>();
        services.AddSingleton<ISupplierDataAccess, SupplierDataAccess>();

        return services;
    }
}
