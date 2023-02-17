using Application.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProfilesController : BaseApiController
    {
        [HttpPost("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            return HandlerResult(await Mediator.Send(new Details.Query { Username = username }));
        }
    }
}
