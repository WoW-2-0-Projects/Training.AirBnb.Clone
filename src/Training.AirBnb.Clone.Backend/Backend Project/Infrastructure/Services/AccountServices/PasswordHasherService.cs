using Backend_Project.Domain.Entities;
using System.Diagnostics;

namespace Backend_Project.Infrastructure.Services.AccountServices;

public static class PasswordHasherService
{
    public static string Hash(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);
    public static bool Verify(string password, string hash) =>
        BCrypt.Net.BCrypt.Verify(password, hash);
}