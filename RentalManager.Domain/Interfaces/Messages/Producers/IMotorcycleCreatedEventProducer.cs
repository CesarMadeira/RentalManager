using RentalManager.Domain.Entities.Events;

namespace RentalManager.Domain.Interfaces.Messages.Producers
{
    public interface IMotorcycleCreatedEventProducer
    {
        Task PublishAsync(MotorcycleEvent motorcycleCreatedEvent);
    }
}
