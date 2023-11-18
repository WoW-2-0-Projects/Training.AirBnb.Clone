namespace AirBnB.Domain.Commons;

public class AuditableEntity : Entity, IAuditableEntity
{
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    
    public DateTime Update { get; set; }
}