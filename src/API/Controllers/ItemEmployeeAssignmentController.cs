using Application.Core;
using Application.ItemEmployeeAssignments;
using Application.ItemEmployeeAssignments.Dto;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class ItemEmployeeAssignmentController : BaseApiController
{
	[HttpGet]
	public async Task<IActionResult> GetItemEmployeeAssignmentsAsync([FromQuery] PagingParams param)
	{
		return HandlerPagedResult(await Mediator.Send(new GetItemAssignementQuery { Params = param }));
	}

	[HttpPost]
	public async Task<IActionResult> CreateItemEmployeeAssignmentAsync(ItemEmployeeAssignmentRequest model)
	{
		return HandlerResult(await Mediator.Send(new CreateItemEmployeeAssignmentCommand { ItemEmployeeAssignment = model }));
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetItemEmployeeAssignmentAsync(Guid id)
	{
		return HandlerResult(await Mediator.Send(new GetItemAssignementDetailsQuery { Id = id }));
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> EditItemEmployeeAssignmentAsync(Guid id, ItemEmployeeAssignment model)
	{
		model.AssigmentId = id;
		return HandlerResult(await Mediator.Send(new EditItemAssignementCommand { ItemEmployeeAssignment = model }));
	}

	[HttpPut("{id}/ReturnItem")]
	public async Task<IActionResult> ReturnItemItemAsync(Guid id, ItemEmployeeAssignment item)
	{
		item.ItemId = id;
		return HandlerResult(await Mediator.Send(new ReturnItemCommand { IItem = item }));
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteItemEmployeeAssignmentAsync(Guid id)
	{
		return HandlerResult(await Mediator.Send(new DeleteItemEmployeeAssignmentCommand { Id = id }));
	}
}
