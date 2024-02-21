namespace AirBnB.Domain.Common.Entities;

/// <summary>
/// Represents an abstract class for entities that include audit information.
/// </summary>
/// 
public abstract class AuditableEntity : SoftDeletedEntity, IAuditableEntity
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTimeOffset CreatedTime { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the entity was last updated.
    /// </summary>
    public DateTimeOffset? ModifiedTime { get; set; }
}