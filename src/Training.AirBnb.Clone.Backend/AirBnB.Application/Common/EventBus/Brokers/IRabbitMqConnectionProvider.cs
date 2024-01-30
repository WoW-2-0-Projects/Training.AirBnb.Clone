using RabbitMQ.Client;

namespace AirBnB.Application.Common.EventBus.Brokers;

/// <summary>
/// Represents an interface for providing RabbitMQ connections and creating channels for communication.
/// </summary>
public interface IRabbitMqConnectionProvider
{
    /// <summary>
    /// Asynchronously creates a RabbitMQ channel for communication.
    /// </summary>
    /// <returns></returns>
    ValueTask<IChannel> CreateChannelAsync();
}