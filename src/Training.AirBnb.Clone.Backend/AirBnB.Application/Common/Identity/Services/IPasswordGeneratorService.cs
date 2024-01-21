using AirBnB.Domain.Entities;

namespace AirBnB.Application.Common.Identity.Services;

public interface IPasswordGeneratorService
{
    string GeneratePassword();

    string GetValidatedPassword(string password, User user);
}