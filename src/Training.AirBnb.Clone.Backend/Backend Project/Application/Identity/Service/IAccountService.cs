using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Identity.Service;

public interface IAccountService
{
    ValueTask<User> CreateUserAsync(User user, string password);
    User GetUserByEmailAddress(string emailAddress);
}