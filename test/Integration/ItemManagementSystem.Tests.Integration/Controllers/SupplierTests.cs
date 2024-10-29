using Application.Suppliers;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Persistence;

namespace ItemManagementSystem.Tests.Integration.Controllers;
public class SupplierTests
{
	[TestFixture]
	public class ResidentControllerTests
	{
		private const string Database = "master";
		private const string Username = "sa";
		private const string Password = "$trongPassword";
		private const ushort MsSqlPort = 1433;

		private WebApplicationFactory<Program> _factory;
		private IContainer _container;

		[OneTimeSetUp]
		public async Task OneTimeSetUp()
		{
			// Set up Testcontainers SQL Server container
			_container = new ContainerBuilder()
					.WithImage("mcr.microsoft.com/mssql/server:2022-latest")
					.WithPortBinding(MsSqlPort, true)
					.WithEnvironment("ACCEPT_EULA", "Y")
					.WithEnvironment("SQLCMDUSER", Username)
					.WithEnvironment("SQLCMDPASSWORD", Password)
					.WithEnvironment("MSSQL_SA_PASSWORD", Password)
					.WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(MsSqlPort))
					.Build();

			//Start Container
			await _container.StartAsync();

			var host = _container.Hostname;
			var port = _container.GetMappedPublicPort(MsSqlPort);

			// Replace connection string in DbContext
			var connectionString = $"Server={host},{port};Database={Database};User Id={Username};Password={Password};TrustServerCertificate=True";
			_factory = new WebApplicationFactory<Program>()
					.WithWebHostBuilder(builder =>
					{
						builder.ConfigureServices(services =>
						{
							services.AddDbContext<DataContext>(options =>
												options.UseSqlServer(connectionString));
						});
					});

			// Initialize database
			using var scope = _factory.Services.CreateScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
			dbContext.Database.Migrate();
		}

		[OneTimeTearDown]
		public async Task OneTimeTearDown()
		{
			await _container.StopAsync();
			await _container.DisposeAsync();
			_factory.Dispose();
		}


		[Test]
		public async Task GetSuppliers_ReturnsSuppliers()
		{
			// Arrange
			var client = _factory.CreateClient();
			var response = await client.GetAsync("supplier");

			// Act
			response.EnsureSuccessStatusCode();
			//var suppliers = await response.Content.ReadAsAsync<List<SupplierDto>>();
			string suppliers = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.That(suppliers, Is.Not.Null);
			Assert.That(suppliers.Count, Is.EqualTo(3));
		}

		//[Test]
		//public async Task GetSupplier_ReturnsSupplier()
		//{
		//	// Arrange
		//	var client = AppFactory.CreateClient();
		//	var response = await client.GetAsync("/api/suppliers/1");

		//	// Act
		//	response.EnsureSuccessStatusCode();
		//	var supplier = await response.Content.ReadAsAsync<SupplierDto>();

		//	// Assert
		//	Assert.That(supplier, Is.Not.Null);
		//	Assert.That(supplier.Id, Is.EqualTo(1));
		//	Assert.That(supplier.Name, Is.EqualTo("Supplier 1"));
		//}

		//[Test]
		//public async Task CreateSupplier_ReturnsCreatedSupplier()
		//{
		//	// Arrange
		//	var client = AppFactory.CreateClient();
		//	var supplier = new SupplierDto { Name = "Supplier 4" };
		//	var content = new StringContent(JsonConvert.SerializeObject(supplier), Encoding.UTF8, "application/json");
		//	var response = await client.PostAsync("/api/suppliers", content);

		//	// Act
		//	response.EnsureSuccessStatusCode();
		//	var createdSupplier = await response.Content.ReadAsAsync<SupplierDto>();

		//	// Assert
		//	Assert.That(createdSupplier, Is.Not.Null);
		//	Assert.That(createdSupplier.Id, Is.GreaterThan(0));
		//	Assert.That(createdSupplier.Name, Is.EqualTo("Supplier 4"));
		//}

		//[Test]
		//public async Task UpdateSupplier_ReturnsUpdatedSupplier()
		//{
		//	// Arrange
		//	var client = AppFactory.CreateClient();
		//	var supplier = new SupplierDto { Id = 1, Name = "Supplier 1 Updated" };
		//	var content = new StringContent(JsonConvert.SerializeObject(supplier), Encoding.UTF8, "application/json");
		//	var response = await client.PutAsync("/api/suppliers/1", content);

		//	// Act
		//	response.EnsureSuccessStatusCode();
		//	var updatedSupplier = await response.Content.ReadAsAsync<SupplierDto>();

		//	// Assert
		//	Assert.That(updatedSupplier, Is.Not.Null);

		//}
	}
}
