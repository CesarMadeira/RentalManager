namespace RentalManager.Domain.Entities;

public class Rent
{
    public Rent(string id, string deliveryPersonId, string motorcycleId,
        DateTime start, DateTime finish, DateTime endForecast, int plan)
    {
        Id = id;
        DeliveryPersonId = deliveryPersonId;
        MotorcycleId = motorcycleId;
        Start = start;
        Finish = finish;
        EndForecast = endForecast;
        Plan = plan;
    }

    public string Id { get; private set; }
    public string DeliveryPersonId { get; private set; }
    public string MotorcycleId { get; private set; }
    public DateTime Start { get; private set; }
    public DateTime Finish { get; private set; }
    public DateTime EndForecast { get; private set; }
    public int Plan { get; private set; }
}
