using AirBnB.Domain.Enums;

namespace AirBnB.Api.Models.DTOs;

public class EmailTemplateDto
{
    public Guid Id { get; set; }

    public string Subject { get; set; } = default!;

    public string Content { get; set; } = default!;

    public NotificationType Type { get; set; }

    public NotificationTemplateType TemplateType { get; set; }
}