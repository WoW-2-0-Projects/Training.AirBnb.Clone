using Backend_Project.Application.Identity.Models;
using Backend_Project.Application.Identity.Service;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;

namespace Backend_Project.Infrastructure.Services.AccountServices
{
    public class AuthService : IAuthService
    {
        private readonly IAccountService _accountService;
        private readonly IAccessTokenGeneratorService _accessTokenGeneratorService;

        public AuthService(IAccountService accountService, IAccessTokenGeneratorService accessTokenGeneratorService)
        {
            _accountService = accountService;
            _accessTokenGeneratorService = accessTokenGeneratorService;
        }

        public async ValueTask<bool> RegisterAsync(RegistrationDetails registrationDetails)
        {
            var newUser = new User
            {
                FirstName = registrationDetails.FirstName,
                LastName = registrationDetails.LastName,
                EmailAddress = registrationDetails.EmailAddress,
            };

            await _accountService.CreateUserAsync(newUser, registrationDetails.Password);

            return true;
        }

        public ValueTask<string> LoginAsync(LoginDetails loginDetails)
        {
            var exsistingUser = _accountService.GetUserByEmailAddress(loginDetails.EmailAddress);

            if (exsistingUser == null)
                throw new EntityNotFoundException<User>("This User does not exsist!!");

            var accessToken =  _accessTokenGeneratorService.GetToken(exsistingUser);

            return new(accessToken);
        }
    }
}