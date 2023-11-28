namespace AirBnB.Domain.Enums;

/// <summary>
/// Represents different events related to entities in the application lifecycle.
/// </summary>
public enum EntityEvent
{
    OnGet,
    OnCreate,
    OnUpdate,
    OnDelete
}