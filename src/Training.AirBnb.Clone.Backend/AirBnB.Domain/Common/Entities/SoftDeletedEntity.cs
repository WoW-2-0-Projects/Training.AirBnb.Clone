namespace AirBnB.Domain.Common;

public abstract class SoftDeletedEntity : Entity, ISoftDeletedEntity
{
    public bool IsDeleted { get; set; }
    
    public DateTimeOffset? DeletedTime { get; set; }
}