namespace AirBnB.Domain.Commons;

public class SoftDeletedEntity: ISoftDeletedEntity
{
    public bool IsDeleted { get; set; }
    
    public DateTimeOffset? DeletedDate { get; set; }
}