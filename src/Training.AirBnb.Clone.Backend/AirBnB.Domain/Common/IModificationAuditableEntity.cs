namespace AirBnB.Domain.Common;

public interface IModificationAuditableEntity
{
    /// <summary>
    /// Gets or sets the identifier of the user who last modified the entity.
    /// </summary>
    /// <value>
    /// The identifier of the user who last modified the entity.
    /// </value>
    public Guid ModifiedBUserId { get; set; }
}