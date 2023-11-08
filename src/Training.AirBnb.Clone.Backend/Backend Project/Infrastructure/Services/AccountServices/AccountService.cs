using Backend_Project.Application.Foundations.AccountServices;
using Backend_Project.Application.Identity;
using Backend_Project.Domain.Entities;

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

        var createdUser = await _userService.CreateAsync(user);

        var userCredintials = new UserCredentials
        {
            UserId = createdUser.Id,
            Password = password
        };

        userCredintials.Password = _passwordHasher.Hash(password);

        await _userCredentialsService.CreateAsync(userCredintials);

        return createdUser;
    }
}