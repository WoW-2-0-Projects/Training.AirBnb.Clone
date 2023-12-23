using Type = AirBnB.Domain.Enums.NotificationType;
using AirBnB.Domain.Common;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents an email template for notifications, implementing the IAuditableEntity interface.
/// </summary>
public class EmailTemplate : NotificationTemplate, IAuditableEntity
{
    /// <summary>
    /// Gets or sets the subject of the email template.
    /// </summary>
    /// <remarks>
    /// The subject is the main content or description of the email message.
    /// </remarks>
    public string Subject { get; set; } = default!;

    /// <summary>
    /// Initializes a new instance of the EmailTemplate class.
    /// </summary>
    /// <remarks>
    /// Sets the type of the template to Type.Email.
    /// </remarks>
    public EmailTemplate() => Type = Type.Email;
}
