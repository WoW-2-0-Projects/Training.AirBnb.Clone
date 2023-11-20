namespace AirBnB.Domain.Common;

public abstract class SoftDeletedEntity :AuditableEntity, ISoftDeletedEntity
{
    public bool IsDeleted { get; set; }
    
    public DateTimeOffset? DeletedTime { get; set; }
}