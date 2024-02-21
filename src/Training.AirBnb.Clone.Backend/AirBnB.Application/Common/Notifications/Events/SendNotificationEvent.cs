using AirBnB.Application.Common.Notifications.Models;

namespace AirBnB.Application.Common.Notifications.Events;

/// <summary>
/// Represents an event for sending notifications, extending the 'NotificationEvent' base class.
/// </summary>
public class SendNotificationEvent : NotificationEvent
{
    /// <summary>
    /// Gets or sets the notification message associated with this event.
    /// </summary>
    public NotificationMessage Message { get; set; } = default!;
}