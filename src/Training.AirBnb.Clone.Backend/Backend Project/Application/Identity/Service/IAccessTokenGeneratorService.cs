using Backend_Project.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Backend_Project.Application.Identity.Service
{
    public interface IAccessTokenGeneratorService
    {
        string GetToken(User user);
        JwtSecurityToken GetJwtToken(User user);
        List<Claim> GetClaims(User user);
    }
}