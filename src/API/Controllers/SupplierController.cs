using Application.Core;
using Application.Suppliers;
using Application.Suppliers.Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SupplierController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetSuppliers([FromQuery] PagingParams param)
    {
        return HandlerPagedResult(await Mediator.Send(new List.Query { Params = param }));
    }

    [HttpPost]
    public async Task<IActionResult> CreateSupplier(SupplierRequest sup)
    {
        return HandlerResult(await Mediator.Send(new Create.Command { SupplierRequest = sup }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditSupplier(Guid id, Supplier sup)
    {
        sup.SupplierId = id;
        return HandlerResult(await Mediator.Send(new Edit.Command { supplier = sup }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSupplier(Guid id)
    {
        return HandlerResult(await Mediator.Send(new Delete.Command { Id = id }));
    }
}

