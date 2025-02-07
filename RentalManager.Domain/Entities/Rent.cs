namespace RentalManager.Domain.Entities;

public class Rent
{
    public Rent(string id, string deliveryPersonId, string motorcycleId,
        DateTime start, DateTime? finish, DateTime endForecast, int plan)
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
    public DateTime? Finish { get; private set; }
    public DateTime EndForecast { get; private set; }
    public int Plan { get; private set; }

    public void EndLease(DateTime date)
    {
        Finish = date;
    }

    public decimal CalculateTotalValue(DateTime? returnDate = null)
    {
        decimal cost = 0;
        switch (Plan)
        {
            case 7:
                cost = 30;
                break;
            case 15:
                cost = 28;
                break;
            case 30:
                cost = 22;
                break;
            case 45:
                cost = 20;
                break;
            case 50:
                cost = 18;
                break;
        }

        DateTime endDate = returnDate.HasValue ? returnDate.Value : EndForecast;
        return ((endDate - Start).Days == 0 ? 1 : (endDate - Start).Days) * cost;
    }
}
