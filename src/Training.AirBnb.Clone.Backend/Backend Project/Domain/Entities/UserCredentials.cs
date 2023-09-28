
using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class UserCredentials:SoftDeletedEntity
{
    public Guid UserId { get; set; }
    public string Password { get; set; }

    public UserCredentials(Guid userId, string password)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Password = password;
        CreatedDate = DateTimeOffset.UtcNow;
    }
}
