using Item.Application.Interface;
using Microsoft.Extensions.DependencyInjection;
using Item.DataAccess;
using Microsoft.Extensions.Configuration;

namespace Item.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationService(this IServiceCollection srv, IConfiguration config)
    {
        srv.AddScoped<IItemService, ItemService>();
        srv.AddDataAccessService(config);

        return srv;
    }
}
