using RentalManager.Application.Commands.Requests;
using RentalManager.Application.Interfaces.Commands;
using RentalManager.Domain.Entities;
using RentalManager.Domain.Exceptions;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Domain.ValueObject;
using System.ComponentModel.DataAnnotations;

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
        if (await _deliveryPersonRepository.GetByCNPJ(new CNPJ(request.CNPJ)) != null)
            throw new BusinessException($"Ja existe um entregador com esse CNPJ: {request.CNPJ}");

        if (await _deliveryPersonRepository.GetByCNH(new CNH(request.DocumentNumber)) != null)
            throw new BusinessException($"Ja existe um entregador com essa CNH: {request.DocumentNumber}");

        var deliveryPerson = new DeliveryPerson(request.Id, request.Name, request.CNPJ, request.DateOfBirth, request.DocumentNumber, request.DocumentType, null);
        await _deliveryPersonRepository.Create(deliveryPerson);
    }
}
