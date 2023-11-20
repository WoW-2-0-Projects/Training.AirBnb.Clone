namespace AirBnB.Domain.Common;

public interface ISoftDeletedEntity : IAuditableEntity 
{
    public bool IsDeleted { get; set; }
    
    public DateTimeOffset? DeletedTime { get; set; }
}