using AirBnB.Domain.Entities;

namespace AirBnB.Application.Common.Notifications.Services;

public interface IAccessTokenGeneratorService
{
    AccessToken GetToken(User user);

    Guid GettokenId(string accessToken);
}