using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Identity.Service;

public interface IAccountService
{
    ValueTask<User> CreateUserAsync(User user, string password);
}