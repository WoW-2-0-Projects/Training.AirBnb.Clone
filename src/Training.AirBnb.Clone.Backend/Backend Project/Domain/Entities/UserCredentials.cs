#pragma warning disable CS8618

using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class UserCredentials:SoftDeletedEntity
{
    public Guid UserId { get; set; }
    public string Password { get; set; }
}