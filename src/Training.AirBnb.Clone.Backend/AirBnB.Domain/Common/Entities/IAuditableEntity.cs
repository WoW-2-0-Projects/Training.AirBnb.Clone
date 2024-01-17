namespace AirBnB.Domain.Common.Entities;

public interface IAuditableEntity : ISoftDeletedEntity
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    /// <value>
    /// The date and time when the entity was created.
    /// </value>
    public DateTimeOffset CreatedTime { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the entity was last updated.
    /// </summary>
    /// <value>
    /// The date and time when the entity was last updated.
    /// </value>
    public DateTimeOffset? ModifiedTime { get; set; }
}