namespace AirBnB.Domain.Commons;

public interface ISoftDeletedEntity 
{ 
    public bool IsDeleted { get; set; }
    
    DateTimeOffset? DeletedDate { get; set; }
}