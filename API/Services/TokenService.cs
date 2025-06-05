using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using API.Interfaces;
using App.Entities;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenkey = config["TokenKey"] ?? throw new Exception("Cannot access Token Key from Appsetting");
        if (tokenkey.Length < 64) throw new Exception("Your token key need to be longer");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey));

        var claim = new List<Claim>
         {
           new Claim(ClaimTypes.NameIdentifier,user.Username)
         };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claim),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };

        var tokenhandler = new JwtSecurityTokenHandler();
        var token = tokenhandler.CreateToken(tokenDescriptor);
        return tokenhandler.WriteToken(token);
    }
}

