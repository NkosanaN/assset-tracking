using Application.Suppliers;
using FluentAssertions;

namespace ItemManagementSystem.Tests.Integrationx.Feature;

//https://www.youtube.com/watch?v=tj5ZCtvgXKY&t=79s

public class SupplierHandler(TestWebApplicationFactory factory) : TestBase(factory)
{
	[Fact]
	public async Task Given_Valid_Supplier_When_CreateSupplierCalled_Should_ReturnSupplier()
	{
		//Arrange
		var requestSupplier = new SupplierDto
		{
			SupplierName = "Supplier_1",
			SupplierDescription = "Supplier Description"
		};

		await Sender.Send(new CreateSupplierCommand { SupplierDto = requestSupplier });

		await Context.SaveChangeAsync(default);

		//Assert
		var response = Context.Suppliers.FirstOrDefault(c => c.SupplierName.Contains(requestSupplier.SupplierName));
		response.Should().NotBeNull();
		response!.SupplierName.Should().Contain(requestSupplier.SupplierName);
		response.SupplierDescription.Should().Contain(requestSupplier.SupplierDescription);
	}

	[Fact]
	public async Task Given_Valid_Supplier_When_EditSupplierCalled_Should_ReturnEditedSupplier()
	{
		//Arrange
		var requestSupplier = new SupplierDto
		{
			SupplierName = "old Supplier",
			SupplierDescription = "Supplier Description"
		};

		await Sender.Send(new CreateSupplierCommand { SupplierDto = requestSupplier });
		await Context.SaveChangeAsync(default);

		//Get a single supplier record under Application folder
		var oldSupplierResult = Context.Suppliers.FirstOrDefault(c => c.SupplierName.Contains(requestSupplier.SupplierName));

		oldSupplierResult!.SupplierName = "new Supplier Updated";
		oldSupplierResult.SupplierDescription = "Supplier Description Updated";

		await Sender.Send(new EditSupplierCommand { Supplier = oldSupplierResult });

		var updatedSupplierResult = Context.Suppliers.FirstOrDefault(c => c.SupplierId == oldSupplierResult.SupplierId);

		//Assert
		updatedSupplierResult.Should().NotBeNull();
		updatedSupplierResult!.SupplierId.Equals(oldSupplierResult.SupplierId);
		updatedSupplierResult.SupplierName.Should().Contain(oldSupplierResult.SupplierName);
	}
}
