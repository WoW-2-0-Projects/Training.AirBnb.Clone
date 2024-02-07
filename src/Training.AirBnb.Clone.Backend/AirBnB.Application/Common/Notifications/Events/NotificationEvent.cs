using AirBnB.Domain.Common.Events;

namespace AirBnB.Application.Common.Notifications.Events;

public class NotificationEvent : Event
{
    public Guid SenderUserId { get; init; }

    public Guid ReceiverUserId { get; init; }
}