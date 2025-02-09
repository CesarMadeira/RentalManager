using Microsoft.Extensions.Options;
using RentalManager.Application.Commands.Handlers;
using RentalManager.Application.Commands.Requests;
using RentalManager.Domain.Interfaces.Messages.Producers;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.Dapper;
using RentalManager.Infra.Messages.Producers;
using RentalManager.Infra.RabbitMQ;
using RentalManager.Infra.RabbitMQ.Model;
using RentalManager.Infra.Repositories;

namespace RentalManagerTest.Commands;

[TestClass]
public class RegisterNewMotorcycleCommandHandlerTest
{
    private IMotorcycleRepository _motorcycleRepository;
    private IMotorcycleCreatedEventProducer _motorcycleCreatedEvent;

    public RegisterNewMotorcycleCommandHandlerTest()
    {
        var conn = new DatabaseConnection("Host=localhost;Port=5432;Database=rentalmanager_db;Username=postgres;Password=postgres");
        _motorcycleRepository = new MotorcycleRepository(conn);

        var rabbitMqSettings = new RabbitMqSettings { Host = "localhost", User = "guest", Password = "guest" };
        var options = Options.Create(rabbitMqSettings);
        _motorcycleCreatedEvent = new MotorcycleCreatedEventProducer(new RabbitMqConnection(options));
    }

    [TestMethod("Deve cadastrar uma nova moto")]
    public async Task ShouldRegisterNewMotorcycle()
    {
        var command = new RegisterNewMotorcycleCommandHandler(_motorcycleRepository, _motorcycleCreatedEvent);
        var request = new RegisterNewMotorcycleCommandRequest { Id = "teste123", Year = 2025, Model = "Sport", LicencePlate = "CDX-0101" };
        await command.Handle(request);

        var motorcycle = await _motorcycleRepository.Get(request.Id);
        await _motorcycleRepository.Delete(request.Id);

        Assert.AreEqual(motorcycle.Id, request.Id);
        Assert.AreEqual(motorcycle.Model, request.Model);
        Assert.AreEqual(motorcycle.LicencePlate, request.LicencePlate);
        Assert.AreEqual(motorcycle.Year, request.Year);
    }

    [TestMethod("Nao deve cadastrar uma placa ja existente")]
    public async Task shouldNotRegisterExistingPlate()
    {
        var command = new RegisterNewMotorcycleCommandHandler(_motorcycleRepository, _motorcycleCreatedEvent);
        var request = new RegisterNewMotorcycleCommandRequest { Id = "teste124", Year = 2025, Model = "Sport", LicencePlate = "CDX-0102" };
        await command.Handle(request);

        string message = string.Empty;
        try
        {
            await command.Handle(request);
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }

        await _motorcycleRepository.Delete(request.Id);

        Assert.AreEqual("Placa já foi cadastrada!", message);
    }
}
