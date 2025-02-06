namespace RentalManager.Application.Commands.Requests;

public class RentMotorcycleCommandRequest
{
    public string Id { get; set; }
    public string DeliveryPersonId { get; set; }
    public string MotorcycleId { get; set; }
    public DateTime Start { get; set; }
    public DateTime Finish { get; set; }
    public DateTime EndForecast { get; set; }
    public int Plan { get; set; }
}
