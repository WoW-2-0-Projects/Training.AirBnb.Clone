using AirBnB.Domain.Enums;

namespace AirBnB.Api.Models.DTOs;

/// <summary>
/// Data transfer object (DTO) representing an email template.
/// </summary>
public class EmailTemplateDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the email template.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the subject of the email template.
    /// </summary>
    public string Subject { get; set; } = default!;

    /// <summary>
    /// Gets or sets the content of the email template.
    /// </summary>
    public string Content { get; set; } = default!;

    /// <summary>
    /// Gets or sets the type of the notification associated with the email template.
    /// </summary>
    public NotificationType Type { get; set; }

    /// <summary>
    /// Gets or sets the template type of the email template.
    /// </summary>
    public NotificationTemplateType TemplateType { get; set; }
}
