using AirBnB.Domain.Common.Events;

namespace AirBnB.Application.Common.Notifications.Events;

/// <summary>
/// Represents a notification event, extending the base class 'Event'.
/// </summary>
public class NotificationEvent : Event
{
    /// <summary>
    /// Gets or initializes the unique identifier of the user sending the notification.
    /// </summary>
    public Guid SenderUserId { get; init; }

    /// <summary>
    /// Gets or initializes the unique identifier of the user receiving the notification.
    /// </summary>
    public Guid ReceiverUserId { get; init; }
}