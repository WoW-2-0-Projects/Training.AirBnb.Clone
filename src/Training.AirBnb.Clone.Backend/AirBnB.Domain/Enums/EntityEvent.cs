namespace AirBnB.Domain.Enums;

/// <summary>
/// Represents different events related to entities in the application lifecycle.
/// </summary>
public enum EntityEvent
{
    /// <summary>
    /// Represents read events related to entities in the application lifecycle.
    /// </summary>
    OnGet,
    
    /// <summary>
    ///   Represents create events related to entities in the application lifecycle.
    /// </summary>
    OnCreate,
    
    /// <summary>
    ///   Represents update events related to entities in the application lifecycle.
    /// </summary>
    OnUpdate,
    
    /// <summary>
    ///   Represents delete events related to entities in the application lifecycle.
    /// </summary>
    OnDelete
}