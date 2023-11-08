using Backend_Project.Application.Identity;

namespace Backend_Project.Infrastructure.Services.AccountServices;

public  class PasswordHasherService : IPasswordHasher
{
    public string Hash(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string password, string hash) =>
        BCrypt.Net.BCrypt.Verify(password, hash);
}