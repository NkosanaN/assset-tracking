//using Application.Core;
//using Application.ShelveTypes;
//using Domain;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Controllers;

//public class ShelveTypeController : BaseApiController
//{
//	[HttpGet]
//	public async Task<IActionResult> GetShelveTypesAsync([FromQuery] PagingParams param)
//	{
//		return HandlerPagedResult(await Mediator.Send(new List.Query { Params = param }));
//	}
//	[HttpGet("{id}")]
//	public async Task<IActionResult> DetailsShelveTypesAsync(Guid id)
//	{
//		return HandlerResult(await Mediator.Send(new Details.Query { ShelfId = id }));
//	}

//	[HttpPost]
//	public async Task<IActionResult> CreateShelveTypesAsync(ShelveType dept)
//	{
//		return HandlerResult(await Mediator.Send(new Create.Command { ShelveType = dept }));
//	}

//	[HttpPut("{id}")]
//	public async Task<IActionResult> EditShelveTypesAsync(Guid id, ShelveType shelve)
//	{
//		shelve.ShelfId = id;
//		return HandlerResult(await Mediator.Send(new Edit.Command { ShelveType = shelve }));
//	}

//	[HttpDelete("{id}")]
//	public async Task<IActionResult> DeleteShelveTypesAsync(Guid id)
//	{
//		return HandlerResult(await Mediator.Send(new Delete.Command { Id = id }));
//	}
//}

