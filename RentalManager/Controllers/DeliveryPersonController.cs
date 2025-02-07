using Microsoft.AspNetCore.Mvc;
using RentalManager.Application.Commands.Requests;
using RentalManager.Application.Interfaces.Commands;
using RentalManager.Domain.Interfaces.Respositories;

namespace RentalManager.Controllers;

[ApiController]
[Route("entregadores")]
public class DeliveryPersonController : ControllerBase
{
    private readonly IRegisterDeliveryPersonCommandHandler _registerDeliveryPersonCommandHandler;

    public DeliveryPersonController(
        IRegisterDeliveryPersonCommandHandler registerDeliveryPersonCommandHandler
    ) {
        _registerDeliveryPersonCommandHandler = registerDeliveryPersonCommandHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Post(RegisterDeliveryPersonCommandRequest request)
    {
        await _registerDeliveryPersonCommandHandler.Handle(request);
        return Ok(new { message = "Entregador cadastrado com sucesso!"});
    }

    [HttpPost("{id}/cnh")]
    public IActionResult SendPhotoOfDocument(string id)
    {
        return Ok();
    }
}
