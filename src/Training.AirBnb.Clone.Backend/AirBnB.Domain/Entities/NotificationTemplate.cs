using AirBnB.Domain.Common;
using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities;

/// <summary>
/// NotificationTemplate abstract model implemented Auditable
/// </summary>
public abstract class NotificationTemplate : AuditableEntity
{
    /// <summary>
    /// Gets or sets the Content of the NotificationTemplate
    /// </summary>
    public string Content { get; set; } = default!;

    /// <summary>
    /// Gets or sets the NotificationType of the NotificationTemplate
    /// </summary>
    public NotificationType Type { get; set; }

    /// <summary>
    /// Gets or sets the NotificationTemplateType of the NotificationTemplate
    /// </summary>
    public NotificationTemplateType TemplateType { get; set; }

    //public IList<NotificationHistory> Histories { get; set; } = new List<NotificationHistory>();
}