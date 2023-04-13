using Application.Core;
using Application.Departments;
using Application.Departments.Contracts;
using Domain;
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

    [HttpPut("{id}")]
    public async Task<IActionResult> EditDepartment(Guid id, Department depart)
    {
        depart.DepartmentId = id;
        return HandlerResult(await Mediator.Send(new Edit.Command { department = depart }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(Guid id)
    {
      return HandlerResult(await Mediator.Send(new Delete.Command { Id = id }));
    }
}

