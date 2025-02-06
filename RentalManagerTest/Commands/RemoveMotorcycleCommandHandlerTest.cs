using RentalManager.Application.Commands.Handlers;
using RentalManager.Application.Commands.Requests;
using RentalManager.Domain.Entities;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.Dapper;
using RentalManager.Infra.Repositories;

namespace RentalManagerTest.Commands;

[TestClass]
public class RemoveMotorcycleCommandHandlerTest
{
    private IMotorcycleRepository _motorcycleRepository;

    public RemoveMotorcycleCommandHandlerTest()
    {
        var conn = new DatabaseConnection("Host=localhost;Port=5432;Database=rentalmanager_db;Username=postgres;Password=postgres");

        _motorcycleRepository = new MotorcycleRepository(conn);
    }

    [TestMethod("Deve remover moto")]
    public async Task ShouldRemoveMotorcycle()
    {
        var motorcycle = new Motorcycle(Guid.NewGuid().ToString(), "ABC-1234", "Sport", 2020);
        await _motorcycleRepository.Create(motorcycle);

        var command = new RemoveMotorcycleCommandHandler(_motorcycleRepository);
        var request = new RemoveMotorcycleCommandRequest { MotorcycleId = motorcycle.Id };
        await command.Handle(request);

        var motorcycleRemoved = await _motorcycleRepository.Get(motorcycle.Id);

        Assert.IsNull(motorcycleRemoved);
    }
}
