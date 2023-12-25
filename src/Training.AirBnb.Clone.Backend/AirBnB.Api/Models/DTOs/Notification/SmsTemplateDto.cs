using AirBnB.Domain.Enums;

namespace AirBnB.Api.Models.DTOs.Notification;

/// <summary>
/// Data transfer object (DTO) representing an SMS template.
/// </summary>
public class SmsTemplateDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the SMS template.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the content of the SMS template.
    /// </summary>
    public string Content { get; set; } = default!;

    /// <summary>
    /// Gets or sets the type of the notification associated with the SMS template.
    /// </summary>
    public NotificationType Type { get; set; }

    /// <summary>
    /// Gets or sets the template type of the SMS template.
    /// </summary>
    public NotificationTemplateType TemplateType { get; set; }
}
