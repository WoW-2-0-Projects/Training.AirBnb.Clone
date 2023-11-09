using Backend_Project.Application.Identity.Models;

namespace Backend_Project.Application.Identity.Service
{
    public interface IAuthService
    {
        ValueTask<bool> RegisterAsync(RegistrationDetails registrationDetails, string password);
        ValueTask<string> LoginAsync(LoginDetails loginDetails);
    }
}