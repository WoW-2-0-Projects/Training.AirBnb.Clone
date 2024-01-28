using AirBnB.Domain.Common.Events;

namespace AirBnB.Application.Common.EventBus;

/// <summary>
/// Represents a generic interface for an event bus broker, which facilitates the publishing and subscribing to events.
/// </summary>
public interface IEvenBusBroker
{
    /// <summary>
    /// Publishes an event locally within the current context.
    /// </summary>
    /// <param name="command"></param>
    /// <typeparam name="TEvent"></typeparam>
    /// <returns></returns>
    ValueTask PublishLocalAsync<TEvent>(TEvent command) where TEvent : IEvent;

    /// <summary>
    /// Publishes an event, making it available for handling by subscribers.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event to be published.</typeparam>
    /// <param name="event">The event to be published.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// Published events are typically broadcasted to subscribers, allowing them to handle the event based on their specific logic.
    /// </remarks>
    ValueTask PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;

   
}
