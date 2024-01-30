namespace AirBnB.Application.Common.EventBus.Brokers;

/// <summary>
/// Represents an interface for an event subscriber, allowing for starting and stopping the subscription to events.
/// </summary>
public interface IEventSubscriber
{
    /// <summary>
    /// Starts the event subscriber asynchronously.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    ValueTask StartAsync(CancellationToken token);

    /// <summary>
    /// Stops the event subscriber asynchronously.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    ValueTask StopAsync(CancellationToken token);
}