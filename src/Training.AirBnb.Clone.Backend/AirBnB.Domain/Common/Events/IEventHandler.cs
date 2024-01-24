using MediatR;

namespace AirBnB.Domain.Common.Events;

public interface IEventHandler<in TEvent> : IEventHandler, INotificationHandler<TEvent> where TEvent : IEvent
{
}

public interface IEventHandler
{
}