using RentalManager.Application.Commands.Handlers;
using RentalManager.Application.Commands.Requests;
using RentalManager.Domain.Entities;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.Dapper;
using RentalManager.Infra.Repositories;

namespace RentalManagerTest.Commands;

[TestClass]
public class ModifyMotorcycleLicensePlateCommandHandlerTest
{
    private IMotorcycleRepository _motorcycleRepository;

    public ModifyMotorcycleLicensePlateCommandHandlerTest()
    {
        var conn = new DatabaseConnection("Host=localhost;Port=5432;Database=rentalmanager_db;Username=postgres;Password=postgres");

        _motorcycleRepository = new MotorcycleRepository(conn);
    }

    [TestMethod("Deve modificar a placa da moto")]
    public async Task ShouldModifyMotorcycleLicensePlate()
    {
        var motorcycle = new Motorcycle(Guid.NewGuid().ToString(), "ABC-1234", "Sport", 2020);
        await _motorcycleRepository.Create(motorcycle);

        var command = new ModifyMotorcycleLicensePlateCommandHandler(_motorcycleRepository);
        var request = new ModifyMotorcycleLicensePlateCommandRequest { MotorcycleId = motorcycle.Id, LicencePlate = "ABC-5678" };
        await command.Handle(request);

        var motorcycleModified = await _motorcycleRepository.Get(motorcycle.Id);
        await _motorcycleRepository.Delete(motorcycle.Id);

        Assert.AreEqual(request.LicencePlate, motorcycleModified.LicencePlate);
    }
}
