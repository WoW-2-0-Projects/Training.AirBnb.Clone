namespace AirBnB.Domain.Common.Entities;

/// <summary>
/// Represents the base class for entities in the system.
/// </summary>
public abstract class Entity : IEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public Guid Id { get; set; }
}