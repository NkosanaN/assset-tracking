using Domain;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Suppliers;
using FluentAssertions;
using Application.Core;

namespace ItemManagementSystem.Tests.Unity.Feature;

public class SupplierHandlerTests : IDisposable
{
	private readonly DataContext _dataContext;
	private readonly GetSupplierQueryHandler _handler;
	private readonly Guid _supplier1Id = Guid.NewGuid();
	private readonly Guid _supplier2Id = Guid.NewGuid();

	public SupplierHandlerTests()
	{
		var option = new DbContextOptionsBuilder<DataContext>()
					.UseInMemoryDatabase(databaseName: $"TestItemTrackerDb_{Guid.NewGuid()}")
				 	.Options;

		_dataContext = new TestDataContext(option);
		_handler = new GetSupplierQueryHandler(_dataContext);

		SeedTestData();
	}

	private void SeedTestData()
	{
		_dataContext.AddRange(
			new Supplier
			{
				SupplierId = _supplier1Id,
				SupplierName = "Supplier_1",
				SupplierDescription = "Supplier Description 1",
				CreatedAt = DateTime.UtcNow
			},
			new Supplier
			{
				SupplierId = _supplier2Id,
				SupplierName = "Supplier_2",
				SupplierDescription = "Supplier Description 2",
				CreatedAt = DateTime.UtcNow.AddDays(-1)
			}
		);

		_dataContext.SaveChanges();
	}

	[Test]
	public async Task Given_Valid_Supplier_When_GetSupplierCalled_Should_ReturnSupplier()
	{
		//Act
		var request = new GetSupplierQuery { Params = new PagingParams { PageNumber = 1, PageSize = 5 } };

		var result = await _handler.Handle(request, default);

		//Assert
		result.IsSuccess.Should().BeTrue();
		result.Error.Should().BeNull();
		result.Value.Count.Should().Be(2);
		result.Value[0].SupplierName.Should().Contain("Supplier_1");
		result.Value[0].SupplierDescription.Should().Contain("Supplier Description");
	}

	[Test]
	public async Task Given_InValid_Supplier_When_GetSupplierCalled_Should_ReturnError()
	{
		// Arrange
		var invalidSupplierId = Guid.NewGuid();
		var request = new GetSupplierDetailQuery { SupplierId = invalidSupplierId };

		// Act
		var result = await _handler.Handle(request, CancellationToken.None);

		// Assert
		result.IsSuccess.Should().BeFalse();
		result.Error.Should().Be($"Supplier with ID {invalidSupplierId} not found");
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
		_dataContext.Database.EnsureDeleted();
		_dataContext.Dispose();
		GC.SuppressFinalize(this);
	}
}
