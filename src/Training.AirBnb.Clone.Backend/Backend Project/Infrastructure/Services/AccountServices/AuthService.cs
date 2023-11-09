using Backend_Project.Application.Identity.Models;
using Backend_Project.Application.Identity.Service;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;

namespace Backend_Project.Infrastructure.Services.AccountServices
{
    public class AuthService : IAuthService
    {
        private readonly IAccountService _accountService;
        private readonly IAccessTokenGeneratorService _accessTokenGeneratorService;
        private readonly IDataContext _dataContext;

        public AuthService(IAccountService accountService, IAccessTokenGeneratorService accessTokenGeneratorService, IDataContext dataContext)
        {
            _accountService = accountService;
            _accessTokenGeneratorService = accessTokenGeneratorService;
            _dataContext = dataContext;
        }

        public async ValueTask<bool> RegisterAsync(RegistrationDetails registrationDetails, string password)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = registrationDetails.FirstName,
                LastName = registrationDetails.LastName,
                EmailAddress = registrationDetails.EmailAddress,
            };

            await _accountService.CreateUserAsync(newUser, password);

            return true;
        }

        public ValueTask<string> LoginAsync(LoginDetails loginDetails)
        {
            var exsistingUser = _dataContext.Users.FirstOrDefault(user => user.EmailAddress == loginDetails.EmailAddress);

            if (exsistingUser == null)
                throw new EntityNotFoundException("This User does not exsist!!");

            var accessToken =  _accessTokenGeneratorService.GetToken(exsistingUser);

            return new(accessToken);
        }
    }
}