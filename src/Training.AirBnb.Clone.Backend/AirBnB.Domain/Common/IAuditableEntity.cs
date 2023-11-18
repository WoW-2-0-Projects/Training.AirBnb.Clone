namespace AirBnB.Domain.Common;

public interface IAuditableEntity : IEntity
{
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}