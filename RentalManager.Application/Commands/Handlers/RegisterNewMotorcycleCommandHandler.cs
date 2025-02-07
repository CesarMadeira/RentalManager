﻿using RentalManager.Application.Commands.Requests;
using RentalManager.Application.Interfaces.Commands;
using RentalManager.Domain.Entities;
using RentalManager.Domain.Interfaces.Respositories;

namespace RentalManager.Application.Commands.Handlers;

public class RegisterNewMotorcycleCommandHandler: IRegisterNewMotorcycleCommandHandler
{
    private readonly IMotorcycleRepository _motorcycleRepository;

    public RegisterNewMotorcycleCommandHandler(IMotorcycleRepository motorcycleRepository)
    {
        _motorcycleRepository = motorcycleRepository;
    }

    public async Task Handle(RegisterNewMotorcycleCommandRequest request)
    {
        // TODO validar campos obrigatorios - Identificador, Ano, Modelo e Placa
        var validateMotocyle = await _motorcycleRepository.GetByLicencePlate(request.LicencePlate);
        if (validateMotocyle != null)
        {
            throw new Exception("Placa já cadastrada, entre em contato com o suporte!");
        }

        var motorcycle = new Motorcycle(request.Id, request.LicencePlate, request.Model, request.Year);

        await _motorcycleRepository.Create(motorcycle);

        // TODO publicar evento de criação da moto
    }
}
