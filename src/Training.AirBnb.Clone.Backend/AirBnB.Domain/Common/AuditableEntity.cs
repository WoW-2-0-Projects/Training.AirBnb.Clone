namespace AirBnB.Domain.Common;

public class AuditableEntity : Entity, IAuditableEntity
{
    public DateTime CreatedTime { get; set; }
    
    public DateTime UpdatedTime { get; set; }
}