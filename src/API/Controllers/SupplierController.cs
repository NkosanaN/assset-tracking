using Application.Core;
using Application.Suppliers;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SupplierController : BaseApiController
{
	[HttpGet]
	public async Task<IActionResult> GetSuppliersAsync([FromQuery] PagingParams param)
	{
		return HandlerPagedResult(await Mediator.Send(new GetSupplierQuery { Params = param }));
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> DetailSupplierAsync(Guid id)
	{
		return HandlerResult(await Mediator.Send(new GetSupplierDetailQuery { SupplierId = id }));
	}

	[HttpPost]
	public async Task<IActionResult> CreateSupplierAsync(SupplierDto sup)
	{
		return HandlerResult(await Mediator.Send(new CreateSupplierCommand { SupplierDto = sup }));
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> EditSupplierAsync(Guid id, Supplier supplier)
	{
		supplier.SupplierId = id;
		return HandlerResult(await Mediator.Send(new EditSupplierCommand { Supplier = supplier }));
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteSupplierAsync(Guid id)
	{
		return HandlerResult(await Mediator.Send(new DeleteSupplierCommand { Id = id }));
	}
}

