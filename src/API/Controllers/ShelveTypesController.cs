﻿using Application.Core;
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
    public async Task<IActionResult> CreateShelveTypes(ShelveType dept)
    {
        return HandlerResult(await Mediator.Send(new Create.Command { ShelveType = dept }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditShelveTypes(Guid id, ShelveType shelve)
    {
        shelve.ShelfId = id;
        return HandlerResult(await Mediator.Send(new Edit.Command { shelvetype = shelve }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShelveTypes(Guid id)
    {
        return HandlerResult(await Mediator.Send(new Delete.Command { Id = id }));
    }
}

