using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AirBnB.Domain.Entities;

namespace AirBnB.Application.Common.Identity.Services;

/// <summary>
/// Interface for a service responsible for generating and managing access tokens.
/// </summary>
public interface IAccessTokenGeneratorService
{
    /// <summary>
    /// Generates an access token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the access token is generated.</param>
    /// <returns>An AccessToken object representing the generated access token.</returns>
    AccessToken GetToken(User user);

    /// <summary>
    /// Retrieves the unique identifier (ID) associated with the provided access token.
    /// </summary>
    /// <param name="accessToken">The access token for which the ID is to be retrieved.</param>
    /// <returns>The unique identifier (ID) associated with the provided access token.</returns>
    Guid GetTokenId(string accessToken);

    /// <summary>
    /// Generates a JWT security token for the specified user and access token.
    /// </summary>
    /// <param name="user">The user for whom the token is generated.</param>
    /// <param name="accessToken">The associated access token.</param>
    /// <returns>A JwtSecurityToken representing the generated JWT token.</returns>
    JwtSecurityToken GetToken(User user, AccessToken accessToken);

    /// <summary>
    /// Retrieves a list of claims associated with the specified user and access token.
    /// </summary>
    /// <param name="user">The user for whom claims are generated.</param>
    /// <param name="accessToken">The associated access token.</param>
    /// <returns>A list of Claim objects representing the generated claims.</returns>
    List<Claim> GetClaims(User user, AccessToken accessToken);
}
