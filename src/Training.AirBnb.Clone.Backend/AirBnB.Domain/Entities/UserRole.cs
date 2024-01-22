using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities;

public class UserRole
{
    //ID of the user account
    public Guid UserId { get; set; }
    
    //ID of the assigned role
    public Guid RoleId { get; set; }
    
    //Navigation property to retrieve full user details 
    public User User { get; set; }
    
    //Navigation property to retrieve details of the assigned role
    public Role Role { get; set; }
}