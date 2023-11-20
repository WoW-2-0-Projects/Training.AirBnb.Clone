namespace AirBnB.Domain.Common;

public interface IAuditableEntity : ISoftDeletedEntity
{
    public DateTimeOffset CreatedTime { get; set; }
    
    public DateTimeOffset? UpdatedTime { get; set; }
}