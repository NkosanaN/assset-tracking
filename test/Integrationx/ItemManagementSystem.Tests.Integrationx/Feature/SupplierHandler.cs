using Application.Core;
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
		var supplier = new SupplierDto
		{
			SupplierName = "Supplier_1",
			SupplierDescription = "Supplier Description"
		};

		var supplierId = await Sender.Send(new CreateSupplierCommand { SupplierDto = supplier });

		await Context.SaveChangeAsync(default);

		var query = new GetSupplierQuery { Params = new PagingParams { PageNumber = 1, PageSize = 5 } };

		//Assert
		var getSupplier = Context.Suppliers.FirstOrDefault(c => c.SupplierName.Contains(supplier.SupplierName));
		getSupplier.Should().NotBeNull();
	}

	//[Fact]
	//public async Task Given_Valid_Supplier_When_CreateSupplierCalled_Should_ReturnSupplier()
	//{
	//	//Arrange
	//	var supplier = new SupplierDto
	//	{
	//		SupplierName = "Supplier_1",
	//		SupplierDescription = "Supplier Description"
	//	};

	//	await Sender.Send(new CreateSupplierCommand { SupplierDto = supplier });

	//	//Assert
	//	var getSupplier = Context.Suppliers.FirstOrDefault(c => c.SupplierName.Contains(supplier.SupplierName));
	//	getSupplier.Should().NotBeNull();
	//}

}
