namespace AirBnB.Domain.Enums;

/// <summary>
/// Enumeration representing different roles within the AirBnB system.
/// </summary>
public enum RoleType
{
    /// <summary>
    /// Represents a regular user role in the system.
    /// </summary>
    User = 0,

    /// <summary>
    /// Represents an administrative role with elevated privileges.
    /// </summary>
    Admin = 2,

    /// <summary>
    /// Represents a system-level role with special permissions.
    /// </summary>
    System = 1
}
