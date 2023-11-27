using AirBnB.Domain.Common;
using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities;

public abstract class NotificationTemplate : AuditableEntity
{
    public string Content { get; set; } = default!;

    public NotificationType Type { get; set; }

    public NotificationTemplateType TemplateType { get; set; }

    //public IList<NotificationHistory> Histories { get; set; } = new List<NotificationHistory>();

}