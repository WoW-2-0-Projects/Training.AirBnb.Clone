﻿namespace AirBnB.Domain.Enums;

/// <summary>
/// Enumeration representing different roles within the AirBnB system.
/// </summary>
public enum RoleType
{
    /// <summary>
    /// Represents a guest role for regular customers or visitors without administrative privileges.
    /// </summary>
    Guest = 0,
    
    /// <summary>
    /// Represents a host role with specific responsibilities
    /// </summary>
    Host = 1,
    
    /// <summary>
    /// Represents an administrative role with elevated privileges.
    /// </summary>
    Admin = 2,
    
    /// <summary>
    /// Represents a system-level role with special permissions.
    /// </summary>
    System = 3,
}
