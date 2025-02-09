using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RentalManager.Domain.Entities.Events;
using RentalManager.Domain.Interfaces.Messages.Consumers;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.RabbitMQ;
using System.Text;
using System.Text.Json;

namespace RentalManager.Infra.Messages.Consumers;

public class MotorcycleCreatedEventConsumer : IMotorcycleCreatedEventConsumer
{
    private readonly IMotorcycleEventRepository _motorcycleEventRepository;
    private IChannel _channel;

    public MotorcycleCreatedEventConsumer(
        RabbitMqConnection mqConnection,
        IMotorcycleEventRepository motorcycleEventRepository
    )
    {
        _channel = mqConnection.GetChannel();
        _motorcycleEventRepository = motorcycleEventRepository;
    }
    public async Task StartConsuming(CancellationToken stoppingToken)
    {
        // TODO alterar o nome da fila
        await _channel.QueueDeclareAsync(queue: "product_queue", durable: false, exclusive: false, autoDelete: false, arguments: null, cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var motorcycleCreatedEvent = JsonSerializer.Deserialize<MotorcycleEvent>(message);

                if (motorcycleCreatedEvent?.Year == 2024)
                {
                    // TODO enviar notificação
                }

                await _motorcycleEventRepository.Create(motorcycleCreatedEvent);

                await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                // Tratar a exceção, logar ou reprocessar
                Console.WriteLine($"Error processing message: {ex.Message}");
            }
        };

        await _channel.BasicConsumeAsync(queue: "product_queue", autoAck: false, consumer: consumer, stoppingToken);
    }
}
