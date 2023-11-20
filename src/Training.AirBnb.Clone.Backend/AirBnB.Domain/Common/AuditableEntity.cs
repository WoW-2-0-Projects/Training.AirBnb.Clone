namespace AirBnB.Domain.Common;

public abstract class AuditableEntity : SoftDeletedEntity, IAuditableEntity
{
    public DateTime CreatedTime { get; set; }
    
    public DateTime? UpdatedTime { get; set; }
}