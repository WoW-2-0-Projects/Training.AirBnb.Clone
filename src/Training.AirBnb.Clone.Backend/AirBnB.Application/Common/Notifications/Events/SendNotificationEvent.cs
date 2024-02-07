using AirBnB.Application.Common.Notifications.Models;

namespace AirBnB.Application.Common.Notifications.Events;

public class SendNotificationEvent : NotificationEvent
{
    public NotificationMessage Message { get; set; } = default!;
}