using RentalManager.Application.Commands.Requests;
using RentalManager.Application.Interfaces.Commands;
using RentalManager.Domain.Entities;
using RentalManager.Domain.Exceptions;
using RentalManager.Domain.Interfaces.Respositories;

namespace RentalManager.Application.Commands.Handlers;

public class RentMotorcycleCommandHandler : IRentMotorcycleCommandHandler
{
    private readonly IRentRepository _rentRepository;
    private readonly IDeliveryPersonRepository _deliveryPersonRepository;
    private readonly IMotorcycleRepository _motorcycleRepository;

    public RentMotorcycleCommandHandler(
        IRentRepository rentRepository,
        IDeliveryPersonRepository deliveryPersonRepository,
        IMotorcycleRepository motorcycleRepository)
    {
        _rentRepository = rentRepository;
        _deliveryPersonRepository = deliveryPersonRepository;
        _motorcycleRepository = motorcycleRepository;
    }

    public async Task Handle(RentMotorcycleCommandRequest request)
    {
        if (request.Plan == 0)
            throw new BusinessException("Escolha um dos planos: 7, 15, 30, 45 ou 50!");

        var motorcycle = await _motorcycleRepository.Get(request.MotorcycleId);
        if (motorcycle == null)
            throw new BusinessException("Moto não cadastrada!");

        var deliveryPerson = await _deliveryPersonRepository.Get(request.DeliveryPersonId);
        if (deliveryPerson == null)
            throw new BusinessException("Entregador não cadastrado!");
        
        if (!deliveryPerson.DocumentType.Contains("A"))
            throw new BusinessException("Categoria da CNH não permitida para a locação!");

        var rent = new Rent(
            request.Id,
            request.DeliveryPersonId,
            request.MotorcycleId,
            StartTheNextDay(request.Start),
            null,
            CalculateForecastDate(request.Start, request.Plan),
            request.Plan);
        
        await _rentRepository.Create(rent);
    }

    public DateTime StartTheNextDay(DateTime date)
    {
        return date.AddDays(1).Date;
    }

    public DateTime CalculateForecastDate(DateTime startDate, int plan)
    {
        return StartTheNextDay(startDate).AddDays(plan).Date;
    }
}
