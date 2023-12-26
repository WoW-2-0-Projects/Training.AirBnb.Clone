using AirBnB.Domain.Common;

namespace AirBnB.Domain.Entities;

public class AccessToken : Entity
{
    public AccessToken()
    {
    }

    public AccessToken(Guid id, Guid userId, string token, DateTimeOffset expiryTime, bool isRevoked)
    {
        Id = id;
        UserId = userId;
        Token = token;
        Expirytime = expiryTime;
        IsRevoked = isRevoked;
    }
    
    public Guid UserId { get; set; }

    public string Token { get; set; } = default!;
    
    public DateTimeOffset Expirytime { get; set; }
    
    public bool IsRevoked { get; set; }
}