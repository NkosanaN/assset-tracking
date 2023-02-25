using Application.Core;
using Application.Items;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ItemsController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetItems([FromQuery] PagingParams param)
    {
        //return HandlerResult(await Mediator.Send(new List.Query { Params = param }));
        return HandlerPagedResult(await Mediator.Send(new List.Query { Params = param }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem(Guid id)
    {
        return HandlerResult(await Mediator.Send(new Details.Query { Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> CreateItem(Item item)
    {
        return HandlerResult(await Mediator.Send(new Create.Command { Item = item }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditItem(Guid id, Item item)
    {
        item.ItemId = id;
        return HandlerResult(await Mediator.Send(new Edit.Command { Item = item }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(Guid id)
    {
        return HandlerResult(await Mediator.Send(new Delete.Command { Id = id }));
    }
}

