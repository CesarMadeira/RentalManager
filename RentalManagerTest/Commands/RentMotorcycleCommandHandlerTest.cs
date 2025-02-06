using RentalManager.Application.Commands.Handlers;
using RentalManager.Application.Commands.Requests;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.Dapper;
using RentalManager.Infra.Repositories;

namespace RentalManagerTest.Commands;

[TestClass]
public class RentMotorcycleCommandHandlerTest
{
    private IRentRepository _RentRepository;

    public RentMotorcycleCommandHandlerTest()
    {
        var conn = new DatabaseConnection("Host=localhost;Port=5432;Database=rentalmanager_db;Username=postgres;Password=postgres");

        _RentRepository = new RentRepository(conn);
    }

    [TestMethod("Deve alugar uma moto")]
    public async Task ShouldRentMotorcycle()
    {
        var command = new RentMotorcycleCommandHandler(_RentRepository);
        var request = new RentMotorcycleCommandRequest
        {
            Id = Guid.NewGuid().ToString(),
            DeliveryPersonId = Guid.NewGuid().ToString(),
            MotorcycleId = Guid.NewGuid().ToString(),
            Start = DateTime.Now.Date,
            Finish = DateTime.Now.AddDays(7).Date,
            EndForecast = DateTime.Now.AddDays(7).Date,
            Plan = 7
        };
        await command.Handle(request);

        var rent = await _RentRepository.Get(request.Id);
        await _RentRepository.Delete(request.Id);

        Assert.AreEqual(rent.Id, request.Id);
        Assert.AreEqual(rent.DeliveryPersonId, request.DeliveryPersonId);
        Assert.AreEqual(rent.MotorcycleId, request.MotorcycleId);
        Assert.AreEqual(rent.Start, request.Start);
        Assert.AreEqual(rent.Finish, request.Finish);
        Assert.AreEqual(rent.EndForecast, request.EndForecast);
        Assert.AreEqual(rent.Plan, request.Plan);
    }
}
