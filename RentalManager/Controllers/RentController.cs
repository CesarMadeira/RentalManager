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
    private readonly IGetRentByIdQueryHandler _getRentByIdQueryHandler;

    public RentController(
        IRentMotorcycleCommandHandler rentMotorcycleCommandHandler,
        ICalculateRentValueByReturnDateQueryHandler calculateRentValueByReturnDateQueryHandler,
        IGetRentByIdQueryHandler getRentByIdQueryHandler)
    {
        _rentMotorcycleCommandHandler = rentMotorcycleCommandHandler;
        _calculateRentValueByReturnDateQueryHandler = calculateRentValueByReturnDateQueryHandler;
        _getRentByIdQueryHandler = getRentByIdQueryHandler;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var response = await _getRentByIdQueryHandler.Handle(new GetRentByIdQueryRequest { RentId = id });
        return Ok(new {data = response});
    }

    [HttpPost]
    public async Task<IActionResult> Post(RentMotorcycleCommandRequest request)
    {
        var response = await _rentMotorcycleCommandHandler.Handle(request);
        return Created("", new { message = "Locação criada com sucesso!", data = response });
    }

    [HttpPut("{id}/devolucao")]
    public async Task<IActionResult> Put(string id, CalculateRentValueByDateQueryRequest request)
    {
        request.RentId = id;
        var response = await _calculateRentValueByReturnDateQueryHandler.Handle(request);
        return Ok(new { data = response });
    }
}
