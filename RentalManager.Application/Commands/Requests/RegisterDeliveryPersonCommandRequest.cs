namespace RentalManager.Application.Commands.Requests;

public class RegisterDeliveryPersonCommandRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string CNPJ { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string DocumentNumber { get; set; }
    public string DocumentType { get; set; }
    public string DocumentImage { get; set; }
}
