using Microsoft.Extensions.DependencyInjection;
using Supplier.DataAccess;

namespace Supplier.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddDataAccessService();
        services.AddSingleton<ISupplierService, SupplierService>();

        return services;
    }
}
