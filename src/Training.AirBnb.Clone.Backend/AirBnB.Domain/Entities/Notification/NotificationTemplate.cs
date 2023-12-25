using AirBnB.Domain.Common;
using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities.Notification;

/// <summary>
/// Represents an abstract base class for notification templates, inheriting from AuditableEntity.
/// </summary>
public abstract class NotificationTemplate : AuditableEntity
{
    /// <summary>
    /// Gets or sets the content of the notification template.
    /// </summary>
    /// <remarks>
    /// The content is the main body or message of the notification.
    /// </remarks>
    public string Content { get; set; } = default!;

    /// <summary>
    /// Gets or sets the type of the notification.
    /// </summary>
    /// <remarks>
    /// The type determines the category or purpose of the notification.
    /// </remarks>
    public NotificationType Type { get; set; }

    /// <summary>
    /// Gets or sets the template type of the notification template.
    /// </summary>
    /// <remarks>
    /// The template type specifies the specific subtype or style of the notification template.
    /// </remarks>
    public NotificationTemplateType TemplateType { get; set; }
}
