using Application.Core;
using Application.ShelveTypes;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ShelveTypesController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetShelveTypes([FromQuery] PagingParams param)
    {
        return HandlerPagedResult(await Mediator.Send(new List.Query { Params = param }));
    }
    [HttpPost]
    public async Task<IActionResult> CreateDepartment(ShelveType dept)
    {
        return HandlerResult(await Mediator.Send(new Create.Command { ShelveType = dept }));
    }


}

