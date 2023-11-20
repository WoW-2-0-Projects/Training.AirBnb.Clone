namespace AirBnB.Domain.Common;

public abstract class AuditableEntity : SoftDeletedEntity, IAuditableEntity
{
    public DateTimeOffset CreatedTime { get; set; }
    
    public DateTimeOffset? UpdatedTime { get; set; }
}