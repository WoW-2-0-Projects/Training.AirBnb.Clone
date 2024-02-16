namespace AirBnB.Api.Models.DTOs;

public class IdentityTokenDto
{
    public string AccessToken { get; set; } = default!;

    public string RefreshToken { get; set; } = default!;
}