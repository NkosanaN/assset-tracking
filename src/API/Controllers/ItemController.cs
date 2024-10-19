using Application.Core;
using Application.Items;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;
public class ItemController : BaseApiController
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


    [HttpPut("{id}/BookRepairItem")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Employee))]
    public async Task<IActionResult> BookRepairItem(Guid id, Item item)
    {
        return HandlerResult(await Mediator.Send(new BookRepair.Command {Item = item}));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(Guid id)
    {
        return HandlerResult(await Mediator.Send(new Delete.Command { Id = id }));
    }
}

