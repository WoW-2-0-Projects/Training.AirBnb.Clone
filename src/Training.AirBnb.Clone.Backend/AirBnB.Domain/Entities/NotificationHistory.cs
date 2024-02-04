using AirBnB.Domain.Common.Entities;
using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents an abstract base class for notification histories
/// </summary>
public abstract class NotificationHistory : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the template
    /// </summary>
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who sent the notification
    /// </summary>
    public Guid SenderUserId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who received notification
    /// </summary>
    public Guid ReceiverUserId { get; set; }

    /// <summary>
    /// Gets or sets type of the notification
    /// </summary>
    public NotificationType Type { get; set; }

    /// <summary>
    /// Gets or sets the content of the notification
    /// </summary>
    public string Content { get; set; } = default!;

    /// <summary>
    /// Gets or sets a flag indicating whether the notification was sent successfully
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Gets or sets an optional error message in case the notification sending was unsuccessful
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets template of the notification
    /// </summary>
    public NotificationTemplate? Template { get; set; }
}
