using RabbitMQ.Client;

namespace AirBnB.Application.Common.EventBus;

/// <summary>
/// Represents an interface for providing RabbitMQ connections and creating channels for communication.
/// </summary>
public interface IRabbitMqConnectionProvider
{
    /// <summary>
    /// Asynchronously creates a RabbitMQ channel for communication.
    /// </summary>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation. The result is the created RabbitMQ channel.</returns>
    /// <remarks>
    /// Creating a channel is an asynchronous operation that allows for the establishment of a communication channel with RabbitMQ for sending and receiving messages.
    /// </remarks>
    ValueTask<IChannel> CreateChannelAsync();
}