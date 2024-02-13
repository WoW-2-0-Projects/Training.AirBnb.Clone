using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Domain.Constants;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Extension;
using AirBnB.Infrastructure.Common.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AirBnB.Infrastructure.Common.Identity.Services;

/// <summary>
/// Service responsible for generating and handling access tokens based on JWT settings.
/// </summary>
public class IdentitySecurityTokenGeneratorService(IOptions<JwtSettings> jwtSettings) : IIdentitySecurityTokenGeneratorService
{
    /// <summary>
    /// Initializes a new instance of the AccessTokenGeneratorService class.
    /// </summary>
    /// <param name="jwtSettings">Options for configuring JWT settings injected via dependency injection.</param>
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    /// <summary>
    /// Generates an access token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the access token is generated.</param>
    /// <returns>An AccessToken object representing the generated access token.</returns>
    public AccessToken GenerateAccessToken(User user)
    {
        // Create a new AccessToken instance with a unique identifier.
        var accessToken = new AccessToken
        {
            Id = Guid.NewGuid()
        };

        // Generate a JWT token for the user and associate it with the AccessToken.
        var jwtToken = GetToken(user, accessToken);

        // Write the JWT token to a string and assign it to the AccessToken's Token property.
        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        accessToken.Token = token;

        // Return the generated AccessToken.
        return accessToken;
    }
    
    /// <summary>
    /// Retrieves the unique identifier (ID) associated with the provided JWT access token.
    /// </summary>
    /// <param name="accessToken">The JWT access token from which to extract the ID.</param>
    /// <returns>The unique identifier (ID) associated with the provided access token.</returns>
    /// <exception cref="ArgumentException">Thrown if the access token is invalid or the associated ID is missing.</exception>
    public Guid GetAccessTokenId(string accessToken)
    {
        // Extract the token value from the authorization header.
        var tokenValue = accessToken.Split(' ')[1];

        // Create a JwtSecurityTokenHandler to read and parse the JWT token.
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(tokenValue);

        // Retrieve the unique identifier (ID) claim from the token.
        var tokenId = token.Claims.FirstOrDefault(c => c.Type == ClaimConstants.AccessTokenId)?.Value;

        // Validate and parse the retrieved ID, throwing an exception if it is invalid or missing.
        if (string.IsNullOrEmpty(tokenId))
            throw new ArgumentException("Invalid AccessToken");

        return Guid.Parse(tokenId);
    }

    /// <summary>
    /// Generates a JWT security token for the specified user and access token.
    /// </summary>
    /// <param name="user">The user for whom the token is generated.</param>
    /// <param name="accessToken">The associated access token.</param>
    /// <returns>A JwtSecurityToken representing the generated JWT token.</returns>
    public JwtSecurityToken GetToken(User user, AccessToken accessToken)
    {
        // Retrieve the claims associated with the user and access token.
        var claims = GetClaims(user, accessToken);
        accessToken.UserId = user.Id;
        accessToken.ExpiryTime = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationTimeInMinutes);
        
        // Create a SymmetricSecurityKey using the specified secret key.
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

        // Create SigningCredentials using the security key and sha 256 algorithm.
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Create and return a new JwtSecurityToken with the specified parameters.
        return new JwtSecurityToken(
            issuer: _jwtSettings.ValidIssuer,
            audience: _jwtSettings.ValidAudience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: accessToken.ExpiryTime.UtcDateTime,
            signingCredentials: credentials
        );
    }

    /// <summary>
    /// Retrieves a list of claims associated with the specified user and access token.
    /// </summary>
    /// <param name="user">The user for whom claims are generated.</param>
    /// <param name="accessToken">The associated access token.</param>
    /// <returns>A list of Claim objects representing the generated claims.</returns>
    public List<Claim> GetClaims(User user, AccessToken accessToken)
    {
        // Create a list of claims with standard and custom claim types.
        return new List<Claim>
        {
            // Claim representing the user's email address.
            new(ClaimTypes.Email, user.EmailAddress),
            
            // Claim representing the user's role.
            //TODO :  fix to add multiple roles
            new(ClaimTypes.Role, user.Roles[0].Type.ToString()),

            // Claim representing the user's unique identifier.
            new(ClaimConstants.UserId, user.Id.ToString()),

            //  This claim uniquely identifies the JWT and is often used to prevent
            new(JwtRegisteredClaimNames.Jti, accessToken.Id.ToString())
        };
    }

    public RefreshToken GenerateRefreshToken(User user, bool extendedExpiryTime = false)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = Convert.ToBase64String(randomNumber),
            UserId = user.Id,
            ExpiryTime = DateTime.UtcNow.AddMinutes(
                extendedExpiryTime
                    ? _jwtSettings.RefreshTokenExtendedExpirationTimeInMinutes
                    : _jwtSettings.RefreshTokenExpirationTimeInMinutes
            )
        };
    }

    public AccessToken? GetAccessToken(string tokenValue)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var getAccessToken = () =>
        {
            var tokenWithoutPrefix = tokenValue.Replace("Bearer ", string.Empty);
            var principal = tokenHandler.ValidateToken(tokenWithoutPrefix, _jwtSettings.MapToTokenValidationParameters(), out var validatedToken);
        
            if (validatedToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase
                ))
                throw new SecurityTokenException("Invalid token");

            return new AccessToken
            {
                Id = Guid.Parse(principal.FindFirst(JwtRegisteredClaimNames.Jti)!.Value),
                UserId = Guid.Parse(principal.FindFirst(ClaimConstants.UserId)!.Value),
                Token = tokenValue,
                ExpiryTime = jwtSecurityToken.ValidTo.ToUniversalTime()
            };
        };

        return getAccessToken.GetValue().Data;

    }
}