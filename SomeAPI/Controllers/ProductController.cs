using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SomeAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{



    [HttpGet]
    [Authorize(Policy = "Read")]
    public IActionResult GetProducts()
    {
        return Ok("All items.");
    }

    [HttpPost]
    [Authorize(Policy = "Upsert")]
    public IActionResult AddProduct()
    {
        return Ok("Created New Item");
    }
    
}