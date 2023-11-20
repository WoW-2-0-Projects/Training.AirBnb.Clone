namespace AirBnB.Domain.Common;

public abstract class AuditableEntity : Entity, IAuditableEntity
{
    public DateTime CreatedTime { get; set; }
    
    public DateTime UpdatedTime { get; set; }
}