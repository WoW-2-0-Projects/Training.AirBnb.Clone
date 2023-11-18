namespace AirBnB.Domain.Common;

public class AuditableEntity : Entity, IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}