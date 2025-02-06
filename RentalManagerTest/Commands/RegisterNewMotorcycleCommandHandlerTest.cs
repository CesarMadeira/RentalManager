using RentalManager.Application.Commands.Handlers;
using RentalManager.Application.Commands.Requests;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.Dapper;
using RentalManager.Infra.Repositories;

namespace RentalManagerTest.Commands;

[TestClass]
public class RegisterNewMotorcycleCommandHandlerTest
{
    private IMotorcycleRepository _motorcycleRepository;

    public RegisterNewMotorcycleCommandHandlerTest()
    {
        var conn = new DatabaseConnection("Host=localhost;Port=5432;Database=rentalmanager_db;Username=postgres;Password=postgres");

        _motorcycleRepository = new MotorcycleRepository(conn);
    }

    [TestMethod("Deve cadastrar uma nova moto")]
    public async Task ShouldRegisterNewMotorcycle()
    {
        var command = new RegisterNewMotorcycleCommandHandler(_motorcycleRepository);
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
        var command = new RegisterNewMotorcycleCommandHandler(_motorcycleRepository);
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

        Assert.AreEqual("Placa já cadastrada, entre em contato com o suporte!", message);
    }
}
