namespace AirBnB.Domain.Common;

/// <summary>
/// Gets or sets the identifier of the user who deleted the entity.
/// </summary>
/// <value>
/// The identifier of the user who deleted the entity.
/// </value>

public interface IDeletionAuditableEntity 
{
    public Guid DeletedByUserId { get; set; }
}