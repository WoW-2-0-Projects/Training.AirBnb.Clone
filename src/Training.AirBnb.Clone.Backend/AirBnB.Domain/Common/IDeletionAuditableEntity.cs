namespace AirBnB.Domain.Common;

public interface IDeletionAuditableEntity 
{
    public Guid DeletedBy { get; set; }
}