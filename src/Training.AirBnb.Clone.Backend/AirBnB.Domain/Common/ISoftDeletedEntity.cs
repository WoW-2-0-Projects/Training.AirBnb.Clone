namespace AirBnB.Domain.Common;

public interface ISoftDeletedEntity : IEntity
{
    public bool IsDeleted { get; set; }
    
    public DateTimeOffset? DeletedTime { get; set; }
}