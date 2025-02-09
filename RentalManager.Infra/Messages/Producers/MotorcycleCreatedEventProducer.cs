using RabbitMQ.Client;
using RentalManager.Domain.Entities.Events;
using RentalManager.Domain.Interfaces.Messages.Producers;
using RentalManager.Infra.RabbitMQ;
using System.Text;
using System.Text.Json;

namespace RentalManager.Infra.Messages.Producers;

public class MotorcycleCreatedEventProducer : IMotorcycleCreatedEventProducer
{
    private IChannel _channel;

    public MotorcycleCreatedEventProducer(RabbitMqConnection mqConnection)
    {
        _channel = mqConnection.GetChannel();
    }

    public async Task PublishAsync(MotorcycleEvent motorcycleCreatedEvent)
    {
        // TODO alterar o nome da fila
        await _channel.QueueDeclareAsync(queue: "product_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

        var message = JsonSerializer.Serialize(motorcycleCreatedEvent);
        var body = Encoding.UTF8.GetBytes(message);

        await _channel.BasicPublishAsync("", "product_queue", mandatory: true, body: body);
    }
}
