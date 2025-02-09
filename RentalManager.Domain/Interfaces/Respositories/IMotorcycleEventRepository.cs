using RentalManager.Domain.Entities.Events;

namespace RentalManager.Domain.Interfaces.Respositories;

public interface IMotorcycleEventRepository
{
    Task Create(MotorcycleEvent motorcycleEvent);
}