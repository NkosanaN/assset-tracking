//using Application.UserPhotos;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Controllers
//{
//    public class UserPhotoController : BaseApiController
//    {
//        [HttpPost]
//        public async Task<IActionResult> Add([FromForm] Add.Command command)
//        {
//            return HandlerResult(await Mediator.Send(command));
//        }
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(string id)
//        {
//            return HandlerResult(await Mediator.Send(new Delete.Command { Id = id }));
//        }
//        [HttpPost("{id}/setMain")]
//        public async Task<IActionResult> SetMain(string id)
//        {
//            return HandlerResult(await Mediator.Send(new SetMain.Command{ Id = id }));
//        }

//    }
//}
