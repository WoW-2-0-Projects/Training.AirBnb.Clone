namespace AirBnB.Domain.Common;

public interface ICreationAuditableEntity
{
    public Guid CreatedBy { get; set; }
}