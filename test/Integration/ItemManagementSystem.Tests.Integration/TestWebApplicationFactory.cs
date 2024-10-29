//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.VisualStudio.TestPlatform.TestHost;
//using Microsoft.Extensions.Configuration;
//using Persistence;

//namespace ItemManagementSystem.Tests.Integration;
//public class TestWebApplicationFactory(string msSqlConnectionString) : WebApplicationFactory<Program>
//{
//	private readonly string _msSqlConnectionString = msSqlConnectionString;

//	protected override void ConfigureWebHost(IWebHostBuilder builder)
//	{
//		builder.ConfigureServices(services =>
//		{
//			var dataStoreContextDescriptor = services.SingleOrDefault(
//				d => d.ServiceType == typeof(DbContextOptions<DataContext>)
//			);

//			if (dataStoreContextDescriptor == null)
//			{
//				return;
//			}

//			services.Remove(dataStoreContextDescriptor);

//			services.AddDbContext<DataContext>(
//				options => options.UseSqlServer(_msSqlConnectionString)
//				);
//		});

//		var appSettings = new List<KeyValuePair<string, string?>>
//		{
//			new("ConnectionStrings:DefaultConnection", _msSqlConnectionString)
//		};

//		var webHostBuilder = builder.ConfigureAppConfiguration(c => c.AddInMemoryCollection(appSettings));

//		base.ConfigureWebHost(builder);
//	}
//}
