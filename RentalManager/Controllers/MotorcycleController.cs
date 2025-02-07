using Microsoft.AspNetCore.Mvc;
using RentalManager.Application.Commands.Requests;
using RentalManager.Application.Interfaces.Commands;
using RentalManager.Application.Interfaces.Queries;
using RentalManager.Application.Queries.Request;

namespace RentalManager.Controllers;

[ApiController]
[Route("motos")]
public class MotorcycleController : ControllerBase
{
    private readonly IGetMotorcycleByIdQueryHandler _getMotorcycleByIdQueryHandler;
    private readonly IRegisterNewMotorcycleCommandHandler _registerNewMotorcycleCommandHandler;
    private readonly ISearchMotorcycleByLicensePlateQueryHandler _searchMotorcycleByLicensePlateQueryHandler;
    private readonly IRemoveMotorcycleCommandHandler _removeMotorcycleCommandHandler;
    private readonly IModifyMotorcycleLicensePlateCommandHandler _modifyMotorcycleLicensePlateCommandHandler;

    public MotorcycleController(IGetMotorcycleByIdQueryHandler getMotorcycleByIdQueryHandler,
        IRegisterNewMotorcycleCommandHandler registerNewMotorcycleCommandHandler,
        ISearchMotorcycleByLicensePlateQueryHandler searchMotorcycleByLicensePlateQueryHandler,
        IRemoveMotorcycleCommandHandler removeMotorcycleCommandHandler,
        IModifyMotorcycleLicensePlateCommandHandler modifyMotorcycleLicensePlateCommandHandler
    ) {
        _getMotorcycleByIdQueryHandler = getMotorcycleByIdQueryHandler;
        _registerNewMotorcycleCommandHandler = registerNewMotorcycleCommandHandler;
        _searchMotorcycleByLicensePlateQueryHandler = searchMotorcycleByLicensePlateQueryHandler;
        _removeMotorcycleCommandHandler = removeMotorcycleCommandHandler;
        _modifyMotorcycleLicensePlateCommandHandler = modifyMotorcycleLicensePlateCommandHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Post(RegisterNewMotorcycleCommandRequest request)
    {
        await _registerNewMotorcycleCommandHandler.Handle(request);
        return Ok(new { message = "Moto cadastrada com sucesso!" });
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? placa)
    {
        var response = await _searchMotorcycleByLicensePlateQueryHandler.Handle(new SearchMotorcycleByLicensePlateQueryRequest
        {
            LicencePlate = placa
        });
        return Ok(response.Item);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _getMotorcycleByIdQueryHandler.Handle(new GetMotorcycleByIdQueryRequest
        {
            MotorcycleId = id
        });
        return Ok(response);
    }

    [HttpPut("{id}/placa")]
    public async Task<IActionResult> Put(string id, ModifyMotorcycleLicensePlateCommandRequest request)
    {
        request.MotorcycleId = id;
        await _modifyMotorcycleLicensePlateCommandHandler.Handle(request);
        return Ok(new { message = "Placa Alterada com sucesso!" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _removeMotorcycleCommandHandler.Handle(new RemoveMotorcycleCommandRequest
        {
            MotorcycleId = id
        });
        return Ok(new { message = "Removido com sucesso!" });
    }
}
