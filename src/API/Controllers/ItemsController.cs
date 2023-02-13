using Application.Items;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : BaseApiController
    {
        // GET: api/<ItemsController>
        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetItems()
        {
            return await Mediator.Send(new List.Query());
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(Guid id)
        {
            return await Mediator.Send(new Details.Query{ Id = id });
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateItem(Item item)
        {
            return Ok(await Mediator.Send(new Create.Command { Item = item }));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditItem(Guid id ,Item item)
        {
            item.Id = id;
            return Ok(await Mediator.Send(new Edit.Command { Item = item }));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            return Ok(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}
