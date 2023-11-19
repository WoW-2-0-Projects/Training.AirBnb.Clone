namespace AirBnB.Domain.Common;

public interface IAuditableEntity : IEntity
{
    public DateTime CreatedTime { get; set; }
    
    public DateTime UpdatedTime { get; set; }
}