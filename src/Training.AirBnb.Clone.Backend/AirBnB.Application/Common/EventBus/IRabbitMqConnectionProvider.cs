using RabbitMQ.Client;

namespace AirBnB.Application.Common.EventBus;

public interface IRabbitMqConnectionProvider
{
    ValueTask<IChannel> CreateChannelAsync();
}