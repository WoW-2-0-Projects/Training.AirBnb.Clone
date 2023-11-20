namespace AirBnB.Domain.Common;

public interface IModificationAuditableEntity
{
    public Guid ModifiedBy { get; set; }
}