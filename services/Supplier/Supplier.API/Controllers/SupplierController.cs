using Microsoft.AspNetCore.Mvc;
using Supplier.Application;

namespace Supplier.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplierController(ISupplierService supplierService) : ControllerBase
{
    private readonly ISupplierService _supplierService = supplierService;

    // GET: api/<SupplierController>
    [HttpGet]
    public async Task<IActionResult> GetSuppliersAsync()
    {
        return Ok(await _supplierService.GetSuppliersAsync());
    }

    // GET api/<SupplierController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<SupplierController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<SupplierController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<SupplierController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}

