using RentalManager.Application.Commands.Handlers;
using RentalManager.Application.Commands.Requests;
using RentalManager.Domain.Entities;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.Dapper;
using RentalManager.Infra.Repositories;

namespace RentalManagerTest.Commands;

[TestClass]
public class RentMotorcycleCommandHandlerTest
{
    private IRentRepository _rentRepository;
    private IDeliveryPersonRepository _deliveryPersonRepository;
    private IMotorcycleRepository _motorcycleRepository;

    public RentMotorcycleCommandHandlerTest()
    {
        var conn = new DatabaseConnection("Host=localhost;Port=5432;Database=rentalmanager_db;Username=postgres;Password=postgres");

        _rentRepository = new RentRepository(conn);
        _deliveryPersonRepository = new DeliveryPersonRepository(conn);
        _motorcycleRepository = new MotorcycleRepository(conn);
    }

    [TestMethod("Deve alugar uma moto")]
    public async Task ShouldRentMotorcycle()
    {
        var deliveryPerson = new DeliveryPerson(Guid.NewGuid().ToString(), "teste", "123456", DateTime.Now, "123456", "A", null);
        await _deliveryPersonRepository.Create(deliveryPerson);

        var motorcycle = new Motorcycle(Guid.NewGuid().ToString(), "CDX-9999", "Sport", 2025);
        await _motorcycleRepository.Create(motorcycle);

        var command = new RentMotorcycleCommandHandler(_rentRepository, _deliveryPersonRepository, _motorcycleRepository);
        var request = new RentMotorcycleCommandRequest
        {
            Id = Guid.NewGuid().ToString(),
            DeliveryPersonId = deliveryPerson.Id,
            MotorcycleId = motorcycle.Id,
            Start = new DateTime(2025, 02, 6),
            Plan = 7
        };
        await command.Handle(request);

        var rent = await _rentRepository.Get(request.Id);
        await _rentRepository.Delete(request.Id);
        await _deliveryPersonRepository.Delete(deliveryPerson.Id);
        await _motorcycleRepository.Delete(motorcycle.Id);

        Assert.AreEqual(rent.Id, request.Id);
        Assert.AreEqual(rent.DeliveryPersonId, request.DeliveryPersonId);
        Assert.AreEqual(rent.MotorcycleId, request.MotorcycleId);
        Assert.AreEqual(rent.Start, new DateTime(2025, 2, 7));
        Assert.IsNull(rent.Finish);
        Assert.AreEqual(rent.EndForecast, new DateTime(2025, 2, 14));
        Assert.AreEqual(rent.Plan, request.Plan);
    }
}
