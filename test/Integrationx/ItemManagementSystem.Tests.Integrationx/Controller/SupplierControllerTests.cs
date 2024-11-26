using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Application.Suppliers;
using Domain;
using FluentAssertions;
using ItemManagementSystem.Tests.Integrationx.Helpers;

namespace ItemManagementSystem.Tests.Integrationx.Controller;
public class SupplierControllerTests(TestWebApplicationFactory factory) : TestBase(factory)
{
	[Fact]
	public async Task Given_Valid_Supplier_When_CreateSupplierCalled_Should_ReturnSupplier()
	{
		//Arrange
		#region
		//var requestSupplierList = new List<SupplierDto>
		//{
		//	new ()
		//	{
		//		SupplierName = "Supplier_1",
		//		SupplierDescription = "Supplier Description"
		//	},
		//	new ()
		//	{
		//		SupplierName = "Supplier_2",
		//		SupplierDescription = "Supplier Description 2"
		//	}
		//};
		#endregion

		var requestSupplier = new SupplierDto
		{
			SupplierName = "Supplier_1",
			SupplierDescription = "Supplier Description"
		};

		var client = factory.CreateClient();

		await Sender.Send(new CreateSupplierCommand { SupplierDto = requestSupplier });

		await Context.SaveChangeAsync(default);

		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TestRunSettings.Token);

		var response = await client.GetAsync("supplier");

		//Act
		response.EnsureSuccessStatusCode();

		string content = await response.Content.ReadAsStringAsync();
		var supplier = JsonSerializer.Deserialize<List<Supplier>>(content, TestSerializerOptions);

		//Assert
		response.StatusCode.Should().Be(HttpStatusCode.OK);
		supplier.Should().NotBeNull();
		supplier.First().SupplierName.Should().Be(requestSupplier.SupplierName);
		supplier.First().SupplierDescription.Should().Be(requestSupplier.SupplierDescription);
	}
}
