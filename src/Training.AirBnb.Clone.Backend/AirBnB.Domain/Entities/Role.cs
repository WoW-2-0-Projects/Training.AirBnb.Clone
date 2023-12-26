using AirBnB.Domain.Common;
using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities;
/// <summary>
/// This class represents a Role entity in the AirBnB application. 
/// </summary>

public class Role : IEntity
{
    // Unique identifier for the Role entity.
    public Guid Id { get; set; }

    // Represents the type of the role (e.g., Admin, User, etc.).
    public RoleType Type { get; set; }

    // Indicates whether the role is disabled or not.
    public bool IsDisable { get; set; }

    // Timestamp indicating when this role was created.
    public DateTime CreatedTime { get; set; }

    // Timestamp indicating the last modification time of this role.
    public DateTime ModifiedTime { get; set; }
}
