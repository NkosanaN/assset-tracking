using Application.Core;
using Application.Suppliers;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> DetailSupplier(Guid id)
    {
        return HandlerResult(await Mediator.Send(new Details.Query { SupplierId = id}));
    }

    [HttpPost]
    public async Task<IActionResult> CreateSupplier(SupplierDto sup)
    {
        return HandlerResult(await Mediator.Send(new Create.Command { SupplierDto = sup }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditSupplier(Guid id, Supplier supplier)
    {
        supplier.SupplierId = id;
        return HandlerResult(await Mediator.Send(new Edit.Command { Supplier = supplier }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSupplier(Guid id)
    {
        return HandlerResult(await Mediator.Send(new Delete.Command { Id = id }));
    }
}

