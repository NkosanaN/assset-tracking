using Item.Application;
using Item.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Item.API.Controllers;

[Route("api/[controller]")]
[ApiController]
//public class ItemManagementController(IItemService itemService) : ControllerBase
//{

public class ItemManagementController : ControllerBase
{

    private readonly IItemService _itemService ;
    public ItemManagementController(IItemService itemService)
    {
        _itemService = itemService;
    }


    // GET: api/<ItemManagementController>
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Domain.Item>))]
    public async Task<IActionResult> GetItemsAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var item = await _itemService.GetItemsAsync();

        return Ok(item);
    }

    // GET api/<ItemManagementController>/5
    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Domain.Item))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]

    public async Task<ActionResult> GetItemsAsync(Guid id, 
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var item = await _itemService.GetItemAsync(id);

        if (item is null)
        {
            return BadRequest();
        }

        return Ok(item);
    }

    // POST api/<ItemManagementController>
    [HttpPost]
    public async Task CreateItemAsync([FromBody] Domain.Item item,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        //var result = await _itemService.CreateItemAsync(item);
    }

    //// PUT api/<ItemManagementController>/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] string value)
    //{
    //}

    //// DELETE api/<ItemManagementController>/5
    //[HttpDelete("{id}")]
    //public void Delete(int id)
    //{
    //}
}
