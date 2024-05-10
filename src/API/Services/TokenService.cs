using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;
public class TokenService(IConfiguration config)
{
    private readonly IConfiguration _config = config;

    public string CreateToken(AppUser appUser)
    {
        //we use Claim to verify is what users say they're
        //We can integrate this with Azure for more security
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, appUser.UserName!),
            new(ClaimTypes.NameIdentifier, appUser.Id),
            new(ClaimTypes.Email, appUser.Email!)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
