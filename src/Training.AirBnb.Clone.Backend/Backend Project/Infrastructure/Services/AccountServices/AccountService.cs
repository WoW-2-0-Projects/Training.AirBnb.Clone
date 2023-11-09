using Backend_Project.Application.Foundations.AccountServices;
using Backend_Project.Application.Identity.Service;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;

namespace Backend_Project.Infrastructure.Services.AccountServices;

public class AccountService : IAccountService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserService  _userService;
    private readonly IUserCredentialsService _userCredentialsService;

    public AccountService(IPasswordHasher passwordHasher, IUserService userService, 
        IUserCredentialsService userCredentialsService)
    {
        _passwordHasher = passwordHasher;
        _userService = userService;
        _userCredentialsService = userCredentialsService;
    }

    public async ValueTask<User> CreateUserAsync(User user, string password)
    {
        user.IsActive = true;

        var passwordStrong = IsStrongPassword(password);

        if (!passwordStrong.IsStrong)
            throw new EntityValidationException($"Password is not strong {passwordStrong.WarningMessage}");

        var createdUser = await _userService.CreateAsync(user);

        var userCredentials = new UserCredentials
        {
            UserId = createdUser.Id,
            Password = _passwordHasher.Hash(password)
        };

        await _userCredentialsService.CreateAsync(userCredentials);

        return createdUser;
    }

    private static (bool IsStrong, string WarningMessage) IsStrongPassword(string password)
    {
        if (password.Length < 8) return (false, "Password can not be less than 8 character");
        if (!password.Any(char.IsDigit)) return (false, "Password should contain at least one digit!");
        if (!password.Any(char.IsUpper)) return (false, "Password should contain at least one upper case letter!");
        if (!password.Any(char.IsLower)) return (false, "Password should contain at least one lower case letter!");
        if (!password.Any(char.IsPunctuation)) return (false, $"Password should contain at least one symbol like {"!@#$%^&?"}!");
        return (true, "");
    }
}