using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Item.DataAccess.Interface;

namespace Item.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccessService(this IServiceCollection srv, IConfiguration config)
    {
        //srv.AddDbContext<DataContext>(opt =>
        //    opt.UseSqlServer(config.GetConnectionString("DefaultConnection"))
        //);

        srv.AddScoped<IItemDataAccess, ItemDataAccess>();

        return srv;
    }
}
