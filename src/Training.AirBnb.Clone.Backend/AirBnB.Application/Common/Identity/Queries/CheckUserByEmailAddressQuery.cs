using AirBnB.Domain.Common.Queries;

namespace AirBnB.Application.Common.Identity.Queries;

/// <summary>
/// Represents check user by email address query
/// </summary>
public record CheckUserByEmailAddressQuery : IQuery<bool>
{
    /// <summary>
    /// Gets user email address
    /// </summary>
    public string EmailAddress { get; init; } = default!;
}