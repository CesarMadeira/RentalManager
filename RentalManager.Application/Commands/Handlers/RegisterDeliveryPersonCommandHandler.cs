using RentalManager.Application.Commands.Requests;
using RentalManager.Application.Interfaces.Commands;
using RentalManager.Domain.Entities;
using RentalManager.Domain.Interfaces.Respositories;

namespace RentalManager.Application.Commands.Handlers;

public class RegisterDeliveryPersonCommandHandler : IRegisterDeliveryPersonCommandHandler
{
    private readonly IDeliveryPersonRepository _deliveryPersonRepository;

    public RegisterDeliveryPersonCommandHandler(IDeliveryPersonRepository deliveryPersonRepository)
    {
        _deliveryPersonRepository = deliveryPersonRepository;
    }

    public async Task Handle(RegisterDeliveryPersonCommandRequest request)
    {
        var deliveryPerson = new DeliveryPerson(request.Id, request.Name, request.CNPJ, request.DateOfBirth, request.DocumentNumber, request.DocumentType, null);
        await _deliveryPersonRepository.Create(deliveryPerson);
    }
}
