using AirBnB.Domain.Common;
namespace AirBnB.Domain.Entities;

public sealed class User : SoftDeletedEntity
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string EmailAddress { get; set; } = default!;

    public string Password { get; set; } = default!;
    
    //TODO: Add User Role for Identity 
    
    public bool IsActive { get; set; }

    public Guid PhoneNumberId { get; set; } = default!;
}