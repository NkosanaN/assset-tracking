using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DbInitializer;

namespace Persistence;
public static class PersistenceServicesRegistration
{
	public static IServiceCollection ConfigurePersistenceService(this IServiceCollection srv,
			IConfiguration config)
	{
		//srv.AddDbContext<DataContext>(opt =>
		//		opt.UseSqlServer(config.GetConnectionString("DefaultConnection"))
		//);

		srv.AddScoped<IDataContext, DataContext>();

		srv.AddDbContext<DataContext>(opt =>
			opt.UseSqlServer(config.GetConnectionString("DefaultConnection"))
	);

		srv.AddScoped<IDbInitializer, DbInitializer.DbInitializer>();

		return srv;
	}
}

