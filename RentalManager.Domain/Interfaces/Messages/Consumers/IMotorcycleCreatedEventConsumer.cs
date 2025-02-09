namespace RentalManager.Domain.Interfaces.Messages.Consumers;

public interface IMotorcycleCreatedEventConsumer
{
    Task StartConsuming(CancellationToken stoppingToken);
}
