using Type = AirBnB.Domain.Enums.NotificationType;
using AirBnB.Domain.Common;

namespace AirBnB.Domain.Entities;

/// <summary>
/// EmailTemplate Model implemented NotificationTemplate abstract model
/// </summary>
public class EmailTemplate : NotificationTemplate, IAuditableEntity
{
    /// <summary>
    /// Gets or sets the Subject of the EmailTemplate
    /// </summary>
    public string Subject { get; set; } = default!;

    /// <summary>
    /// Set Type prperty
    /// </summary>
    public EmailTemplate() => Type = Type.Email;
}