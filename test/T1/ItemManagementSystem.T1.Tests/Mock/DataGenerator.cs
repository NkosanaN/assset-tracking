using Application.Contracts.Persistence;
using Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Moq;

namespace ItemManagementSystem.T1.Tests.Mock;

public static class MockSupplierRepository
{
    //public static Mock<ISupplierRepository> GetMockSupplierRepository(int num)
    //{
    //    var list = new List<Supplier>();

    //    for (int i = 0; i < num; i++)
    //    {
    //     list.Add(new Supplier
    //     {
    //         SupplierId = Guid.NewGuid(),
    //         SupplierName = $"Supplier_{1}",
    //         SupplierDescription = "Supplier Description"
    //     });   
    //    }

    //    var mockRepo = new Mock<ISupplierRepository>();
    //    mockRepo.Setups(r=> r.GetAllSupplier()).Returns(list);

            
    //}

}


