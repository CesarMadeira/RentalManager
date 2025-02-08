using Microsoft.AspNetCore.Mvc;
using RentalManager.Application.Commands.Requests;
using RentalManager.Application.Interfaces.Commands;
using RentalManager.Application.Interfaces.Queries;
using RentalManager.Application.Queries.Request;

namespace RentalManager.Controllers;

[ApiController]
[Route("locacao")]
public class RentController : ControllerBase
{
    private readonly IRentMotorcycleCommandHandler _rentMotorcycleCommandHandler;
    private readonly ICalculateRentValueByReturnDateQueryHandler _calculateRentValueByReturnDateQueryHandler;

    public RentController(
        IRentMotorcycleCommandHandler rentMotorcycleCommandHandler,
        ICalculateRentValueByReturnDateQueryHandler calculateRentValueByReturnDateQueryHandler)
    {
        _rentMotorcycleCommandHandler = rentMotorcycleCommandHandler;
        _calculateRentValueByReturnDateQueryHandler = calculateRentValueByReturnDateQueryHandler;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post(RentMotorcycleCommandRequest request)
    {
        await _rentMotorcycleCommandHandler.Handle(request);
        return Created("", new { message = "Locação criada com sucesso!" });
    }

    [HttpPut("{id}/devolucao")]
    public async Task<IActionResult> Put(string id, CalculateRentValueByDateQueryRequest request)
    {
        request.RentId = id;
        var response = await _calculateRentValueByReturnDateQueryHandler.Handle(request);
        return Ok(new { data = response });
    }
}
