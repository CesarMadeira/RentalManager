using RentalManager.Domain.Entities;
using RentalManager.Domain.Entities.Events;

namespace RentalManager.Domain.Mappers
{
    public static class MotocycleMapper
    {
        public static MotorcycleEvent MotorcycleToMotorcycleCreatedEvent(this Motorcycle motorcycle)
        {
            return new MotorcycleEvent(
                Guid.NewGuid().ToString(),
                motorcycle.Id,
                motorcycle.LicencePlate,
                motorcycle.Model,
                motorcycle.Year,
                "created",
                DateTime.Now);
        }
    }
}
