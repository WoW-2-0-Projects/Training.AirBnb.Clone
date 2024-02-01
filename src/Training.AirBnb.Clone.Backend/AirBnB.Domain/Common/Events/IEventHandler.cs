using MediatR;

namespace AirBnB.Domain.Common.Events;

/// <summary>
/// Represents an interface for handling a specific type of event.
/// </summary>
/// <typeparam name="TEvent"></typeparam>
public interface IEventHandler<in TEvent> : IEventHandler, INotificationHandler<TEvent> where TEvent : IEvent
{
}

/// <summary>
/// Marker interface for event handlers.
/// </summary>
public interface IEventHandler
{
}