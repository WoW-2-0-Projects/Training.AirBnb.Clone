using AirBnB.Application.Common.EventBus.Brokers;
using AirBnB.Infrastructure.Common.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace AirBnB.Infrastructure.Common.EventBus.Brokers;

public class RabbitMqConnectionProvider : IRabbitMqConnectionProvider
{
    private ConnectionFactory _connectionFactory;

    private IConnection? _connection;

    public RabbitMqConnectionProvider(IOptions<RabbitMqConnectionSettings> rabbitMqConnectionSettings)
    {
        var rabbitMqConnectionSettings1 = rabbitMqConnectionSettings.Value;

        _connectionFactory = new ConnectionFactory()
        {
            HostName = rabbitMqConnectionSettings1.HostName,
            Port =  rabbitMqConnectionSettings1.Port
        };

    }
    
    public async ValueTask<IChannel> CreateChannelAsync()
    {
        _connection ??= await _connectionFactory.CreateConnectionAsync();

        return await _connection.CreateChannelAsync();
    }
}