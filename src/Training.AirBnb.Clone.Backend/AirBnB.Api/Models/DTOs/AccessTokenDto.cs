namespace AirBnB.Api.Models.DTOs;

public class AccessTokenDto
{
    public string Token { get; set; } = default!;
    
    public DateTimeOffset ExpiryTime { get; set; }
}