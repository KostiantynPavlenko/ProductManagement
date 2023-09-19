using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.Application.Identity.Interfaces;
using ProductManagement.Domain.Interfaces;

namespace ProductManagement.Infrastructure.Identity.Services;

public class TokenService : ITokenService
{
    private readonly IDateTime _dateTimeService;

    public TokenService(IDateTime dateTimeService)
    {
        _dateTimeService = dateTimeService;
    }
    public string GenerateToken(string username, string email)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Email, email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperNonReachableSecret"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = _dateTimeService.Now.AddMinutes(15),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}