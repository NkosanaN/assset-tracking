using Application.Core;
using Application.ItemEmployeeAssignments;
using Application.ItemEmployeeAssignments.Dto;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class ItemEmployeeAssignmentController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetItemEmployeeAssignments([FromQuery] PagingParams param)
    {
        return HandlerPagedResult(await Mediator.Send(new List.Query { Params = param }));
    }

    [HttpPost]
    public async Task<IActionResult> CreateItemEmployeeAssignment(ItemEmployeeAssignmentRequest model)
    {
       return HandlerResult(await Mediator.Send(new Create.Command { ItemEmployeeAssignment = model }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemEmployeeAssignment(Guid id)
    {
        return HandlerResult(await Mediator.Send(new Details.Query { Id = id }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditItemEmployeeAssignment(Guid id, ItemEmployeeAssignment model)
    {
        model.AssigmentId = id;
        return HandlerResult(await Mediator.Send(new Edit.Command { ItemEmployeeAssignment = model }));
    }

    [HttpPut("{id}/ReturnItem")]
    public async Task<IActionResult> ReturnItemItem(Guid id, ItemEmployeeAssignment item)
    {
        item.ItemId = id;
        return HandlerResult(await Mediator.Send(new ReturnItem.Command { iItem = item}));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItemEmployeeAssignment(Guid id)
    {
        return HandlerResult(await Mediator.Send(new Delete.Command { Id = id }));
    }
}
