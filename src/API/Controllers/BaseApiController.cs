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
        protected IMediator Mediator => _mediator ??=
            HttpContext.RequestServices.GetService<IMediator>();
        
        protected IActionResult HandlerResult<T>(Result<T> result)
        {
            if (result is null) return NotFound();

            if (result.IsSuccess && result.Value != null)
                return Ok(result.Value);

            if (result!.IsSuccess && result.Value == null)
                return NotFound();

            return BadRequest(result.Error);

        }
    }
}
