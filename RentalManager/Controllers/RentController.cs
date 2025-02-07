using Microsoft.AspNetCore.Mvc;
using RentalManager.Application.Interfaces.Queries;
using RentalManager.Application.Queries.Request;

namespace RentalManager.Controllers;

[ApiController]
[Route("locacao")]
public class RentController : ControllerBase
{
    private readonly ICalculateRentValueByReturnDateQueryHandler _calculateRentValueByReturnDateQueryHandler;

    public RentController(ICalculateRentValueByReturnDateQueryHandler calculateRentValueByReturnDateQueryHandler)
    {
        _calculateRentValueByReturnDateQueryHandler = calculateRentValueByReturnDateQueryHandler;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Post()
    {
        return Ok();
    }

    [HttpPut("{id}/devolucao")]
    public async Task<IActionResult> Put(string id, CalculateRentValueByDateQueryRequest request)
    {
        request.RentId = id;
        var response = await _calculateRentValueByReturnDateQueryHandler.Handle(request);
        return Ok(response);
    }
}
