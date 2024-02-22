using AirBnB.Domain.Common.Queries;

namespace AirBnB.Application.Common.Identity.Queries;

/// <summary>
/// Represents user checking query that returns user's firstname if exists
/// </summary>
public record CheckUserByEmailAddressQuery : IQuery<string?>
{
    /// <summary>
    /// Gets user email address
    /// </summary>
    public string EmailAddress { get; init; } = default!;
}