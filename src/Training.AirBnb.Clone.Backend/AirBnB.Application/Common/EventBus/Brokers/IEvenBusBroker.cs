using AirBnB.Domain.Common.Events;

namespace AirBnB.Application.Common.EventBus.Brokers;

/// <summary>
/// Represents a generic interface for an event bus broker, which facilitates the publishing and subscribing to events.
/// </summary>
public interface IEvenBusBroker
{
    ValueTask PublishLocalAsync<TEvent>(TEvent command) where TEvent : IEvent;

    ValueTask PublishAsync<TEvent>(TEvent @event, string exchange, string routingKey, CancellationToken cancellationToken) where TEvent : Event;
}
