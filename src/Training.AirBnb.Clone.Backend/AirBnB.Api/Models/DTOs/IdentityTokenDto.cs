namespace AirBnB.Api.Models.DTOs;

/// <summary>
/// Data transfer object (DTO) representing an identity token, which has access and refresh tokens
/// </summary>
public class IdentityTokenDto
{
    /// <summary>
    /// Gets or sets access token string
    /// </summary>
    public string AccessToken { get; set; } = default!;

    /// <summary>
    /// Gets or sets refresh token string
    /// </summary>
    public string RefreshToken { get; set; } = default!;
}