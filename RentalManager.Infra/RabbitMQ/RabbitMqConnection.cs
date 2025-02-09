using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RentalManager.Infra.RabbitMQ.Model;

namespace RentalManager.Infra.RabbitMQ;

public class RabbitMqConnection
{
    private IConnection _connection;

    public RabbitMqConnection(IOptions<RabbitMqSettings> rabbitMqSettings)
    {
        ConnectionFactory factory = new ConnectionFactory
        {
            HostName = rabbitMqSettings.Value.Host,
            UserName = rabbitMqSettings.Value.User,
            Password = rabbitMqSettings.Value.Password
        };
        _connection = factory.CreateConnectionAsync().Result;
    }

    public IChannel GetChannel()
    {   
        return _connection.CreateChannelAsync().Result;
    }
}
