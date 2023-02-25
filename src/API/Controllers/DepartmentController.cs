using Application.Core;
using Application.Departments;
using Application.Departments.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class DepartmentController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetDepartment([FromQuery] PagingParams param)
    {
        return HandlerPagedResult(await Mediator.Send(new List.Query { Params = param }));
    }
    [HttpPost]
    public async Task<IActionResult> CreateDepartment(DepartmentRequest dept)
    {
        return HandlerResult(await Mediator.Send(new Create.Command { DepartmentRequest = dept }));
    }

  
}

