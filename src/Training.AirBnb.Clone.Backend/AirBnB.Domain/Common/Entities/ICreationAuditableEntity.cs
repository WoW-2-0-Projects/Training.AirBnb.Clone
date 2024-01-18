namespace AirBnB.Domain.Common.Entities;

/// <summary>
/// Gets or sets the identifier of the user who created the entity.
/// </summary>
/// <value>
/// The identifier of the user who created the entity.
/// </value>
public interface ICreationAuditableEntity
{
    public Guid CreatedByUserId { get; set; }
}