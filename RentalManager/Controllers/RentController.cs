using Microsoft.AspNetCore.Mvc;

namespace RentalManager.Controllers;

[ApiController]
[Route("locacao")]
public class RentController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Post()
    {
        return Ok();
    }

    [HttpPut("{id}/devolucao")]
    public IActionResult Put()
    {
        return Ok();
    }
}
