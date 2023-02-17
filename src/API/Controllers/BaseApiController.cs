using API.Extensions;
using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator? _mediator;

        /*
         * First check whether we've Mediator which is active if not request new Mediator
         */
#pragma warning disable CS8603 // Possible null reference return.
        protected IMediator Mediator => _mediator ??=
            HttpContext.RequestServices.GetService<IMediator>();
#pragma warning restore CS8603 // Possible null reference return.

        protected IActionResult HandlerResult<T>(Result<T> result)
        {
            if (result is null) return NotFound();

            if (result.IsSuccess && result.Value != null)
                return Ok(result.Value);

            if (result!.IsSuccess && result.Value == null)
                return NotFound();

            return BadRequest(result.Error);

        }

        protected IActionResult HandlerPagedResult<T>(Result<PagedList<T>> result)
        {
            if (result is null) return NotFound();

            if (result.IsSuccess && result.Value != null)
            {
                Response.AddPaginationHeader(
                    result.Value.CurrentPage,
                    result.Value.PageSize,
                    result.Value.TotalCount,
                    result.Value.TotalPages);
             
                return Ok(result.Value);
            }

            if (result!.IsSuccess && result.Value == null)
                return NotFound();

            return BadRequest(result.Error);

        }
    }
}
