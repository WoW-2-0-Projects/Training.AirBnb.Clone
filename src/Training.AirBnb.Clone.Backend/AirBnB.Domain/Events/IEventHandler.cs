using MediatR;

namespace AirBnB.Domain.Events;

public interface IEventHandler<in TEvent> : IEventHandler, INotificationHandler<TEvent> where TEvent : IEvent
{
}

public interface IEventHandler
{
}