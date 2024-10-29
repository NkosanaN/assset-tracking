using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Testcontainers.MsSql;

namespace ItemManagementSystem.Tests.Integrationx;
public class TestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
	private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder()
			.WithImage("mcr.microsoft.com/mssql/server:2022-latest")
			.WithPortBinding(1433, true)
			.WithEnvironment("ACCEPT_EULA", "Y")
			.WithPassword("$trongPassword")
			.WithName($"ItemTransfer{DateTime.UtcNow.GetHashCode()}")
			.Build();

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureServices(services =>
		{
			var descriptor = services.SingleOrDefault(
				d => d.ServiceType ==
					typeof(DbContextOptions<DataContext>)
			);

			if (descriptor is not null)
			{
				services.Remove(descriptor);
			}

			services.AddDbContext<DataContext>(
				options => options.UseSqlServer(_sqlContainer.GetConnectionString())
				);
		});
	}

	public Task InitializeAsync()
	{
		return _sqlContainer.StartAsync();
	}

	public new Task DisposeAsync()
	{
		return _sqlContainer.StopAsync();
	}
}
