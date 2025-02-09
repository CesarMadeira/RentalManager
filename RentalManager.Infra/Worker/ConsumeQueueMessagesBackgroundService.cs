using Microsoft.Extensions.Hosting;
using RentalManager.Domain.Interfaces.Messages.Consumers;

namespace RentalManager.Infra.Worker
{
    public class ConsumeQueueMessagesBackgroundService : BackgroundService
    {
        private readonly IMotorcycleCreatedEventConsumer _consumer;

        public ConsumeQueueMessagesBackgroundService(IMotorcycleCreatedEventConsumer consumer)
        {
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.StartConsuming(stoppingToken);
        }
    }
}
