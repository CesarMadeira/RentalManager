using RentalManager.Domain.Exceptions;

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

    public decimal CalculateRentalValueForecast(DateTime endDate)
    {
        if (endDate > EndForecast)
        {
            return CalculateRentalValueForecast() + ((endDate - EndForecast).Days * 50);
        }
        else
        {
            return ((decimal)((EndForecast - endDate).Days * PlanValue() * ApplyFine())) + ((endDate - Start).Days * PlanValue());
        }
    }

    public decimal CalculateRentalValueForecast()
    {
        return (EndForecast - Start).Days * PlanValue();
    }

    private int PlanValue()
    {
        switch (Plan)
        {
            case 7:
                return 30;
            case 15:
                return 28;
            case 30:
                return 22;
            case 45:
                return 20;
            case 50:
                return 18;
        }
        throw new BusinessException("Plano não encontrado!");
    }

    private double ApplyFine()
    {
        const double TWENTY_PERCENTAGE_FINE = 0.2;
        const double FORTY_PERCENTAGE_FINE = 0.4;

        switch (Plan)
        {
            case 7:
                return TWENTY_PERCENTAGE_FINE;
            case 15:
                return FORTY_PERCENTAGE_FINE;
        }
        throw new BusinessException($"Porcentagem não definida para esse plano: {Plan.ToString()}");
    }
}
