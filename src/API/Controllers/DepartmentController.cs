using Application.Core;
using Application.Departments;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> DetailsDepartment(Guid id)
    {
        return HandlerResult(await Mediator.Send(new Details.Query { DepartmentId = id }));
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartment(DepartmentDto dept)
    {
        return HandlerResult(await Mediator.Send(new Create.Command { DepartmentRequest = dept }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditDepartment(Guid id, Department department)
    {
        department.DepartmentId = id;
        return HandlerResult(await Mediator.Send(new Edit.Command { Department = department }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(Guid id)
    {
      return HandlerResult(await Mediator.Send(new Delete.Command { Id = id }));
    }
}

