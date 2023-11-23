using AirBnB.Domain.Common;
using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities;

public sealed class User : SoftDeletedEntity
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string EmailAddress { get; set; } = default!;

    public UserRole UserRole { get; set; } = default!;
    
    public bool IsActive { get; set; }

    public Guid PhoneNumberId { get; set; } = default!;
}