using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Eng_Backend.DAL.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Eng_Backend.BusinessLayer.Utils;

public class JwtHelper
{
    private readonly IConfiguration _configuration;

    public JwtHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(ApplicationUser user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FullName)
        };

        // Add role claims for authorization
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenValue = _configuration["AppSettings:Token"];
        if (string.IsNullOrEmpty(tokenValue))
            throw new InvalidOperationException("JWT secret is missing");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenValue));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1), 
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}