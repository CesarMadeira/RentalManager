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
    private IRentRepository _rentRepository;

    public RemoveMotorcycleCommandHandlerTest()
    {
        var conn = new DatabaseConnection("Host=localhost;Port=5432;Database=rentalmanager_db;Username=postgres;Password=postgres");

        _motorcycleRepository = new MotorcycleRepository(conn);
        _rentRepository = new RentRepository(conn);
    }

    [TestMethod("Deve remover moto")]
    public async Task ShouldRemoveMotorcycle()
    {
        var motorcycle = new Motorcycle(Guid.NewGuid().ToString(), "ABC-1234", "Sport", 2020);
        await _motorcycleRepository.Create(motorcycle);

        var command = new RemoveMotorcycleCommandHandler(_motorcycleRepository, _rentRepository);
        var request = new RemoveMotorcycleCommandRequest { MotorcycleId = motorcycle.Id };
        await command.Handle(request);

        var motorcycleRemoved = await _motorcycleRepository.Get(motorcycle.Id);

        Assert.IsNull(motorcycleRemoved);
    }

    [TestMethod("Deve tentar remover moto que não existe")]
    public async Task ShouldTryRemoveMotorcycle()
    {
        string message = string.Empty;
        try
        {
            var command = new RemoveMotorcycleCommandHandler(_motorcycleRepository, _rentRepository);
            var request = new RemoveMotorcycleCommandRequest { MotorcycleId = Guid.NewGuid().ToString() };
            await command.Handle(request);
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }

        Assert.AreEqual("Moto não existe!", message);
    }

    [TestMethod("Deve tentar remover moto que ja foi alugada")]
    public async Task ShouldTryRemoveMotorcycleWithRentals()
    {
        var motorcycle = new Motorcycle(Guid.NewGuid().ToString(), "ABC-1234", "Sport", 2020);
        await _motorcycleRepository.Create(motorcycle);

        var rent = new Rent(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), motorcycle.Id, DateTime.Now, DateTime.Now, DateTime.Now, 7);
        await _rentRepository.Create(rent);

        var command = new RemoveMotorcycleCommandHandler(_motorcycleRepository, _rentRepository);
        var request = new RemoveMotorcycleCommandRequest { MotorcycleId = motorcycle.Id };

        string message = string.Empty;
        try
        {
            await command.Handle(request);
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }

        await _motorcycleRepository.Delete(motorcycle.Id);
        await _rentRepository.Delete(rent.Id);

        Assert.AreEqual("Não foi possivel remover, moto ja foi alugada!", message);
    }
}
