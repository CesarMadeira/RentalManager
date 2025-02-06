using RentalManager.Application.Commands.Requests;
using RentalManager.Application.Interfaces.Commands;
using RentalManager.Domain.Entities;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.Repositories;

namespace RentalManager.Application.Commands.Handlers;

public class RentMotorcycleCommandHandler : IRentMotorcycleCommandHandler
{
    private readonly IRentRepository _rentRepository;

    public RentMotorcycleCommandHandler(IRentRepository rentRepository)
    {
        _rentRepository = rentRepository;
    }

    public async Task Handle(RentMotorcycleCommandRequest request)
    {
        // TODO validar se motoboy existe
        // TODO validar se moto existe
        var rent = new Rent(request.Id, request.DeliveryPersonId, request.MotorcycleId, request.Start, request.Finish, request.EndForecast, request.Plan);
        await _rentRepository.Create(rent);
    }
}
