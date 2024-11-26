using System.Linq.Expressions;
using Application.Contracts.Persistence;
using Application.Suppliers;
using Domain;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Persistence;

namespace ItemManagementSystem.Tests.Unity.Feature;

public class EditSupplierHandlerTests : IDisposable
{
	private readonly IDataContext _dataContext;
	private readonly EditSupplierHandler _handler;

	public EditSupplierHandlerTests()
	{
		// Setup in-memory database
		var options = new DbContextOptionsBuilder<DataContext>()
				.UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
				.Options;
		_dataContext = new DataContext(options);
		_handler = new EditSupplierHandler(_dataContext);
	}

	[Test]
	public async Task Handle_ValidSupplier_UpdatesSuccessfully()
	{
		// Arrange
		var supplier = new Supplier
		{
			SupplierId = Guid.NewGuid(),
			SupplierName = "Original Name",
			SupplierDescription = "Original Description"
		};
		await _dataContext.Suppliers.AddAsync(supplier);
		await _dataContext.SaveChangeAsync(CancellationToken.None);

		var updatedSupplier = new Supplier
		{
			SupplierId = supplier.SupplierId,
			SupplierName = "Updated Name",
			SupplierDescription = "Updated Description"
		};

		// Act
		var result = await _handler.Handle(
				new EditSupplierCommand { Supplier = updatedSupplier },
				CancellationToken.None
		);

		// Assert
		Assert.That(result.IsSuccess, Is.True);
		var dbSupplier = await _dataContext.Suppliers.FirstAsync(s => s.SupplierId == supplier.SupplierId);
		Assert.That(dbSupplier.SupplierName, Is.EqualTo("Updated Name"));
		Assert.That(dbSupplier.SupplierDescription, Is.EqualTo("Updated Description"));
	}

	[Test]
	public async Task Handle_NonExistentSupplier_ReturnsNull()
	{
		// Arrange
		var nonExistentSupplier = new Supplier
		{
			SupplierId = Guid.NewGuid(),
			SupplierName = "Test Name",
			SupplierDescription = "Test Description"
		};

		// Act
		var result = await _handler.Handle(
				new EditSupplierCommand { Supplier = nonExistentSupplier },
				CancellationToken.None
		);

		// Assert
		Assert.That(result, Is.Null);
	}

	[Test]
	public async Task Handle_SaveChangesFails_ReturnsFailureResult()
	{
		// Arrange
		var context = Substitute.For<IDataContext>();
		var dbSet = Substitute.For<DbSet<Supplier>>();
		var supplier = new Supplier
		{
			SupplierId = Guid.NewGuid(),
			SupplierName = "Test Name",
			SupplierDescription = "Test Description"
		};

		dbSet.FirstAsync(Arg.Any<Expression<Func<Supplier, bool>>>(),
				Arg.Any<CancellationToken>())
				.Returns(supplier);

		context.Suppliers.Returns(dbSet);
		context.SaveChangeAsync(Arg.Any<CancellationToken>()).Returns(0);

		var handler = new EditSupplierHandler(context);

		// Act
		var result = await handler.Handle(
				new EditSupplierCommand { Supplier = supplier },
				CancellationToken.None
		);

		// Assert
		Assert.That(result.IsSuccess, Is.False);
		Assert.That(result.Error, Is.EqualTo("Failed to update Supplier"));
	}

	public void Dispose()
	{
		_dataContext.Database.EnsureDeleted();
		_dataContext.Dispose();
		GC.SuppressFinalize(this);
	}
}