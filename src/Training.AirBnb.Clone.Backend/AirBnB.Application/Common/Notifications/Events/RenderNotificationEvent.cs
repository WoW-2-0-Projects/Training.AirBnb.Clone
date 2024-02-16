using AirBnB.Domain.Entities;

namespace AirBnB.Application.Common.Notifications.Events;

/// <summary>
/// Represents an event for rendering notifications, extending the 'NotificationEvent' base class.
/// </summary>
public class RenderNotificationEvent : NotificationEvent
{
    /// <summary>
    /// Gets or sets the notification template associated with this event.
    /// </summary>
    public NotificationTemplate Template { get; set; } = default!;

    /// <summary>
    /// Gets or initializes the sender user associated with this notification event.
    /// </summary>
    public User SenderUser { get; init; } = default!;

    /// <summary>
    /// Gets or initializes the receiver user associated with this notification event.
    /// </summary>
    public User ReceiverUser { get; init; } = default!;

    /// <summary>
    /// Gets or sets the variables associated with the notification event.
    /// The dictionary represents placeholders and their corresponding values.
    /// </summary>
    public Dictionary<string, string> Variables { get; set; } = new();
}