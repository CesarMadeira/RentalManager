namespace RentalManager.Domain.Entities.Events;

public class MotorcycleEvent
{
    public MotorcycleEvent(string id, string mortorcycleId, string licencePlate, string model, int year, string type, DateTime createdAt)
    {
        Id = id;
        MortorcycleId = mortorcycleId;
        LicencePlate = licencePlate;
        Model = model;
        Year = year;
        Type = type;
        CreatedAt = createdAt;
    }

    public string Id { get; private set; }
    public string MortorcycleId { get; private set; }
    public string LicencePlate { get; private set; }
    public string Model { get; private set; }
    public int Year { get; private set; }
    public string Type { get; private set; }
    public DateTime CreatedAt { get; private set; }
}
