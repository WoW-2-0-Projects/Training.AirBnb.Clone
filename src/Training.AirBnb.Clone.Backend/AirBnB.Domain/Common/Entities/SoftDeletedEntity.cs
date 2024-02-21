namespace AirBnB.Domain.Common.Entities;

public abstract class SoftDeletedEntity : Entity, ISoftDeletedEntity
{
    public bool IsDeleted { get; set; }
    
    public DateTimeOffset? DeletedTime { get; set; }
}