//using Application.Contracts.Persistence;
//using Application.Core;
//using Domain;
//using NSubstitute;
//using static ItemManagementSystem.Tests.Unity.Faker;
//using static Application.Items.Create;
//using FluentAssertions;
//using MediatR;
//using Persistence;
//using Application.Suppliers;

//namespace ItemManagementSystem.T1.Tests.Feature;

//public class ItemHandler
//{
//	private DataContext _dataContext;
//	private GetSupplierQueryHandler _handler;
//	private GetSupplierQuery _request;

//	public ItemHandler()
//	{
//		//_dataContext = Substitute.For<DataContext>();
//		//_itemRepositoryMock = new ItemRepository(_dataContext);
//		_itemRepositoryMock = Substitute.For<IItemRepository>();
//		_handler = new Handler(_itemRepositoryMock);
//	}
//	[Test]
//	public async Task Given_Valid_Item_When_CreateItemCalled_Should_CreateItem()
//	{
//		//Arrange
//		var item = new Item
//		{
//			ItemId = Guid.NewGuid(),
//			Name = ItemName(),
//			Description = ItemDescription(),
//			Serialno = ItemSerialno(),
//			ItemTag = ItemTag(),
//			Cost = 5.0f,
//			Qty = 2,
//			DatePurchased = DateTime.Now,
//			DueforRepair = false,
//			ShelfId = Guid.NewGuid(),
//			CreatedById = "TestUser",
//			CreatedBy = new AppUser { UserName = "TestUser" },
//			ShelveBy = new ShelveType { ShelfId = Guid.NewGuid(), ShelfTag = "TestShelf" }
//		};

//		_itemRepositoryMock.IsItemNameUnique(item.Name).Returns(true);
//		_itemRepositoryMock.CreateAsync(item).Returns(true);


//		var result = await _handler.Handle(new Command { Item = item }, default);

//		//Assert
//		result.Should().BeOfType<Result<Unit>>();
//		result.IsSuccess.Should().BeTrue();
//		//result.Value.Should().Be("")
//	}

//}
