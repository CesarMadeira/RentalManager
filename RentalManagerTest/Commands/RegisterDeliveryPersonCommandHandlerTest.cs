using RentalManager.Application.Commands.Handlers;
using RentalManager.Application.Commands.Requests;
using RentalManager.Domain.Entities;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.Dapper;
using RentalManager.Infra.Repositories;

namespace RentalManagerTest.Commands;

[TestClass]
public class RegisterDeliveryPersonCommandHandlerTest
{
    private IDeliveryPersonRepository _deliveryPersonRepository;

    public RegisterDeliveryPersonCommandHandlerTest()
    {
        var conn = new DatabaseConnection("Host=localhost;Port=5432;Database=rentalmanager_db;Username=postgres;Password=postgres");

        _deliveryPersonRepository = new DeliveryPersonRepository(conn);
    }

    [TestMethod("Deve cadastrar um entregador")]
    public async Task ShouldRegisterDeliveryPerson()
    {
        var command = new RegisterDeliveryPersonCommandHandler(_deliveryPersonRepository);
        var request = new RegisterDeliveryPersonCommandRequest
        {
            Id = Guid.NewGuid().ToString(),
            Name = "João da Silva",
            CNPJ = "81388311000171",
            DateOfBirth = DateTime.Now,
            DocumentNumber = "34569709670",
            DocumentType = "A"
        };
        await command.Handle(request);

        var deliveryPerson = await _deliveryPersonRepository.Get(request.Id);
        await _deliveryPersonRepository.Delete(request.Id);

        Assert.AreEqual(deliveryPerson.Id, request.Id);
        Assert.AreEqual(deliveryPerson.Name, request.Name);
        Assert.AreEqual(deliveryPerson.CNPJ.Value, request.CNPJ);
        Assert.AreEqual(deliveryPerson.DateOfBirth.Date, request.DateOfBirth.Date);
        Assert.AreEqual(deliveryPerson.DocumentNumber.Value, request.DocumentNumber);
        Assert.AreEqual(deliveryPerson.DocumentType, request.DocumentType);
    }

    [TestMethod("Deve validar um cadastro de categoria diferente A, B e A+B")]
    public async Task ShouldValidateCategory()
    {
        var command = new RegisterDeliveryPersonCommandHandler(_deliveryPersonRepository);
        var request = new RegisterDeliveryPersonCommandRequest
        {
            Id = Guid.NewGuid().ToString(),
            Name = "João da Silva",
            CNPJ = "81388311000171",
            DateOfBirth = DateTime.Now,
            DocumentNumber = "34569709670",
            DocumentType = "C"
        };
        
        string message = string.Empty;
        try
        {
            await command.Handle(request);
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }

        Assert.AreEqual("Categoria da CNH não permitida!", message);
    }


    [TestMethod("Deve validar se cnpj ja existe")]
    public async Task ShouldValidateIfCNPJExists()
    {
        var newDeliveryPerson = new DeliveryPerson(Guid.NewGuid().ToString(), null, "71843024000150", DateTime.Now, "12327943590", "A", null);
        await _deliveryPersonRepository.Create(newDeliveryPerson);

        var command = new RegisterDeliveryPersonCommandHandler(_deliveryPersonRepository);
        var request = new RegisterDeliveryPersonCommandRequest
        {
            Id = Guid.NewGuid().ToString(),
            Name = "João da Silva",
            CNPJ = "71843024000150",
            DateOfBirth = DateTime.Now,
            DocumentNumber = "12327943590",
            DocumentType = "A"
        };

        string message = string.Empty;
        try
        {
            await command.Handle(request);
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }

        await _deliveryPersonRepository.Delete(newDeliveryPerson.Id);

        Assert.AreEqual($"Ja existe um entregador com esse CNPJ: {request.CNPJ}", message);
    }

    [TestMethod("Deve validar se cnh ja existe")]
    public async Task ShouldValidateIfCNHExists()
    {
        var newDeliveryPerson = new DeliveryPerson(Guid.NewGuid().ToString(), null, "29562457000171", DateTime.Now, "05369875840", "A", null);
        await _deliveryPersonRepository.Create(newDeliveryPerson);

        var command = new RegisterDeliveryPersonCommandHandler(_deliveryPersonRepository);
        var request = new RegisterDeliveryPersonCommandRequest
        {
            Id = Guid.NewGuid().ToString(),
            Name = "João da Silva",
            CNPJ = "26868865000168",
            DateOfBirth = DateTime.Now,
            DocumentNumber = "05369875840",
            DocumentType = "A"
        };

        string message = string.Empty;
        try
        {
            await command.Handle(request);
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }

        await _deliveryPersonRepository.Delete(newDeliveryPerson.Id);

        Assert.AreEqual($"Ja existe um entregador com essa CNH: {request.DocumentNumber}", message);
    }
}
