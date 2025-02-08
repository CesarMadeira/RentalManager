using RentalManager.Application.Commands.Handlers;
using RentalManager.Application.Commands.Requests;
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
}
