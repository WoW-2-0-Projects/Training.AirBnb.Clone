using AirBnB.Domain.Common.Entities;
using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities;
/// <summary>
/// This class represents a Role entity in the AirBnB application. 
/// </summary>

public class Role : AuditableEntity
{
    /// <summary>
    /// Represents the type of the role (e.g., Admin, User, etc.).
    /// </summary>
    public RoleType Type { get; set; }

    /// <summary>
    /// Indicates whether the role is disabled or not. 
    /// </summary>
    public bool IsDisabled { get; set; }
    
    public IList<UserRole> Users { get; set; }
}
