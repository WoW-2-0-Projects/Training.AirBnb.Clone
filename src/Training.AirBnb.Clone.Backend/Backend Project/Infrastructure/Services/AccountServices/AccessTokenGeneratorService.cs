using Backend_Project.Application.Identity.Constants;
using Backend_Project.Application.Identity.Service;
using Backend_Project.Application.Identity.Settings;
using Backend_Project.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend_Project.Infrastructure.Services.AccountServices
{
    public class AccessTokenGeneratorService : IAccessTokenGeneratorService
    {
        private readonly JwtSettings _jwtSettings;

        public AccessTokenGeneratorService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GetToken(User user)
        {
            var jwtToken = GetJwtToken(user);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }

        public JwtSecurityToken GetJwtToken(User user)
        {
            var claims = GetClaims(user);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credintials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _jwtSettings.ValidIssuer,
                audience: _jwtSettings.ValidAudience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationTimeInMinutes),
                signingCredentials: credintials);
        }

        public List<Claim> GetClaims(User user)
        {
            return new List<Claim>()
            {
                new (ClaimTypes.Email, user.EmailAddress),
                new (ClaimConstant.UserId, user.Id.ToString()),
                new (ClaimTypes.Role, user.UserRole.ToString())
            };
        }
    }
}