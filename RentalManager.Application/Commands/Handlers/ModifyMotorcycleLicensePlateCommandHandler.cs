using RentalManager.Application.Commands.Requests;
using RentalManager.Application.Interfaces.Commands;
using RentalManager.Domain.Exceptions;
using RentalManager.Domain.Interfaces.Respositories;

namespace RentalManager.Application.Commands.Handlers;

public class ModifyMotorcycleLicensePlateCommandHandler : IModifyMotorcycleLicensePlateCommandHandler
{
    private readonly IMotorcycleRepository _motorcycleRepository;

    public ModifyMotorcycleLicensePlateCommandHandler(IMotorcycleRepository motorcycleRepository)
    {
        _motorcycleRepository = motorcycleRepository;
    }

    public async Task Handle(ModifyMotorcycleLicensePlateCommandRequest request)
    {
        var motorcycle = await _motorcycleRepository.Get(request.MotorcycleId);
        
        if (motorcycle == null)
        {
            throw new BusinessException("Moto não encontrada!");
        }

        motorcycle.SetLicencePlate(request.LicencePlate);
        await _motorcycleRepository.Save(motorcycle);
    }
}
