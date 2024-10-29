using Domain;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Suppliers;
using Application.Core;
using FluentAssertions;

namespace ItemManagementSystem.Tests.Unity.Feature;

public class SupplierHandler : IDisposable
{
	private DataContext _dataContext;
	private GetSupplierQueryHandler _handler;
	private GetSupplierQuery _request;

	[SetUp]
	public void Setup()
	{
		var option = new DbContextOptionsBuilder<DataContext>()
				 .UseInMemoryDatabase(databaseName: "TestItemTrackerDb")
				 .Options;

		_dataContext = new TestDataContext(option);
		_dataContext.AddRange(
				new Supplier { SupplierId = Guid.NewGuid(), SupplierName = "Supplier_1", SupplierDescription = "Supplier Description" },
				new Supplier { SupplierId = Guid.NewGuid(), SupplierName = "Supplier_2", SupplierDescription = "Supplier Description" }
		 );

		_dataContext.SaveChanges();

		_handler = new GetSupplierQueryHandler(_dataContext);
		_request = new GetSupplierQuery { Params = new PagingParams { PageNumber = 1, PageSize = 5 } };
	}

	[Test]
	public async Task Given_Valid_Supplier_When_GetSupplierCalled_Should_ReturnSupplier()
	{
		//Act
		var result = await _handler.Handle(_request, default);

		//Assert
		result.IsSuccess.Should().BeTrue();
		result.Error.Should().BeNull();
		result.Value.Count.Should().Be(2);
		result.Value[0].SupplierName.Should().Contain("Supplier_1");
		result.Value[0].SupplierDescription.Should().Contain("Supplier Description");
	}

	public class TestDataContext : DataContext
	{
		public TestDataContext(DbContextOptions<DataContext> options)
				: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<Supplier>().HasKey(s => s.SupplierId);
		}
	}

	public void Dispose()
	{
		throw new NotImplementedException();
	}
}
