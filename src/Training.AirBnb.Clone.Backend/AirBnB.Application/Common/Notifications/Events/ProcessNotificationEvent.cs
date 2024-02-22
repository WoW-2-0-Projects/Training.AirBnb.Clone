using AirBnB.Domain.Enums;

namespace AirBnB.Application.Common.Notifications.Events;

/// <summary>
/// Represents an event for processing notifications, extending the 'NotificationEvent' base class.
/// </summary>
public class ProcessNotificationEvent : NotificationEvent
{
    /// <summary>
    /// Gets or initializes the type of notification template associated with this event.
    /// </summary>
    public NotificationTemplateType TemplateType { get; init; }

    /// <summary>
    /// Gets or sets type of the notification.
    /// </summary>
    public NotificationType? Type { get; set; }

    /// <summary>
    /// Gets or sets the optional variables associated with the notification event.
    /// The dictionary represents placeholders and their corresponding values.
    /// </summary>
    public Dictionary<string, string>? Variables { get; set; }
}