using Application.Core;
using Application.Items;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class ItemController : BaseApiController
{
	[HttpGet]
	public async Task<IActionResult> GetItemsAsync([FromQuery] PagingParams param)
	{
		return HandlerPagedResult(await Mediator.Send(new GetItemQuery { Params = param }));
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetItemAsync(Guid id)
	{
		return HandlerResult(await Mediator.Send(new GetItemDetailsQuery { Id = id }));
	}

	[HttpPost]
	public async Task<IActionResult> CreateItemAsync(Item item)
	{
		return HandlerResult(await Mediator.Send(new CreateItemCommand { Item = item }));
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> EditItemAsync(Guid id, Item item)
	{
		item.ItemId = id;
		return HandlerResult(await Mediator.Send(new EditItemCommand { Item = item }));
	}

	[HttpPut("{id}/BookRepairItem")]
	public async Task<IActionResult> BookRepairItemAsync(Item item)
	{
		return HandlerResult(await Mediator.Send(new BookRepairCommand { Item = item }));
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteItemAsync(Guid id)
	{
		return HandlerResult(await Mediator.Send(new DeleteItemCommand { Id = id }));
	}
}

