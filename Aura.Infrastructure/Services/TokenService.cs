using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aura.Application.Interfaces;
using Aura.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Aura.Infrastructure.Services;

public class TokenService : ITokenService
{
    public string GenerateToken(Teacher teacher)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, teacher.Name),
                new Claim(ClaimTypes.Email, teacher.Email),
                new Claim(ClaimTypes.NameIdentifier, teacher.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature),
            Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}