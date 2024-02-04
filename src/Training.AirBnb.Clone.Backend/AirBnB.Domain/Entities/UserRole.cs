using System.ComponentModel.DataAnnotations.Schema;
using AirBnB.Domain.Common.Entities;
using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities;
/// <summary>
/// Represents a user role entity
/// </summary>
public class UserRole : IEntity
{
    
    public UserRole()
    {
        
    }

    /// <summary>
    /// Constructs a new UserRole instance, associating a user with a specific role.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to be assigned the role.</param>
    /// <param name="roleId">The unique identifier of the role to be assigned to the user.</param>
    /// <remarks>
    /// This constructor utilizes a concise expression-bodied syntax to directly initialize the 
    /// `UserId` and `RoleId` properties of the UserRole object upon creation.
    /// </remarks>
    public UserRole(Guid userId, Guid roleId) => (UserId, UserId) = (userId, roleId);
    
    /// <summary>
    /// Gets or sets the user id
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Gets or sets the role id
    /// </summary>
    public Guid RoleId { get; set; }

    [NotMapped]
    public Guid Id { get; set; }
}