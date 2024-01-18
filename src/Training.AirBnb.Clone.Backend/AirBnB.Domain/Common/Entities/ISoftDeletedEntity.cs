namespace AirBnB.Domain.Common.Entities;

public interface ISoftDeletedEntity : IEntity 
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity is deleted.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the entity is deleted; otherwise, <c>false</c>.
    /// </value>
    public bool IsDeleted { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the entity was deleted.
    /// </summary>
    /// <value>
    /// The date and time when the entity was deleted.
    /// </value>
    public DateTimeOffset? DeletedTime { get; set; }
}