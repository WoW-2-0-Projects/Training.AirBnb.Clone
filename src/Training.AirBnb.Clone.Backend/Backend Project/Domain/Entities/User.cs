

using Backend_Project.Domain.Common;
using Backend_Project.Domain.Enums;

namespace Backend_Project.Domain.Entities;

public class User : SoftDeletedEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public UserRole UserRole { get; set; }
    public bool IsActive { get; set; }
    public Guid PhoneNumberId { get; set; }

    public User(string firstName, string lastName, string 
        emailAddress, UserRole userRole, bool isActive, Guid phoneNumberId)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        UserRole = userRole;
        IsActive = isActive;
        PhoneNumberId = phoneNumberId;
        CreatedDate = DateTimeOffset.UtcNow;
    }
}