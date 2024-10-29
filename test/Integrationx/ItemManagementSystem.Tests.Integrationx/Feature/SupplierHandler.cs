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

	//[Fact]
	//public async Task Given_InValid_Supplier_When_CreateSupplierCalled_Should_ReturnError()
	//{
	//	//Arrange
	//	var supplier = new SupplierDto();

	//	await Sender.Send(new CreateSupplierCommand { SupplierDto = supplier });
	//	await Context.SaveChangeAsync(default);

	//	//Assert
	//	var getSupplier = Context.Suppliers.FirstOrDefault(c => c.SupplierName.Contains(supplier.SupplierName));
	//	getSupplier.Should().NotBeNull();
	//}

}
