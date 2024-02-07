using MediatR;

namespace AirBnB.Domain.Common.Events;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}