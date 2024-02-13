using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Brokers;

public interface IRequestUserContextProvider
{
    Guid GetUserId();

    RoleType GetUserRole();

    string? GetAccessToken();
}