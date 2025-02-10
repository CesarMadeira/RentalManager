using Microsoft.AspNetCore.Mvc;
using RentalManager.Application.Commands.Requests;
using RentalManager.Application.Interfaces.Commands;

namespace RentalManager.Controllers;

[ApiController]
[Route("entregadores")]
public class DeliveryPersonController : ControllerBase
{
    private readonly IRegisterDeliveryPersonCommandHandler _registerDeliveryPersonCommandHandler;
    private readonly ISendPhotoOfDocumentCommandHandler _sendPhotoOfDocumentCommandHandler;

    public DeliveryPersonController(
        IRegisterDeliveryPersonCommandHandler registerDeliveryPersonCommandHandler,
        ISendPhotoOfDocumentCommandHandler sendPhotoOfDocumentCommandHandler
    ) {
        _registerDeliveryPersonCommandHandler = registerDeliveryPersonCommandHandler;
        _sendPhotoOfDocumentCommandHandler = sendPhotoOfDocumentCommandHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Post(RegisterDeliveryPersonCommandRequest request)
    {
        await _registerDeliveryPersonCommandHandler.Handle(request);
        return Created("", new { message = "Entregador cadastrado com sucesso!"});
    }

    [HttpPost("{id}/cnh")]
    public async Task<IActionResult> SendPhotoOfDocument(string id, SendPhotoOfDocumentCommandRequest request)
    {
        request.Id = id;
        await _sendPhotoOfDocumentCommandHandler.Handle(request);
        return Ok(new { message = "Foto atualizada com sucesso!" });
    }
}