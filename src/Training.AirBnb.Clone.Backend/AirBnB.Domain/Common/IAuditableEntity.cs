namespace AirBnB.Domain.Common;

public interface IAuditableEntity : ISoftDeletedEntity
{
    public DateTime CreatedTime { get; set; }
    
    public DateTime? UpdatedTime { get; set; }
}