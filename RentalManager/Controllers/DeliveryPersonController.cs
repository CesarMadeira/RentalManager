using Microsoft.AspNetCore.Mvc;
using RentalManager.Domain.Interfaces.Respositories;

namespace RentalManager.Controllers;

[ApiController]
[Route("entregadores")]
public class DeliveryPersonController : ControllerBase
{

    [HttpPost]
    public IActionResult Post()
    {
        return Ok();
    }

    [HttpPost("{id}/cnh")]
    public IActionResult SendPhotoOfDocument(string id)
    {
        return Ok();
    }
}
