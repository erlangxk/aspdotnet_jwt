using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Slot.Api.Conf;


namespace Slot.Api.Services;

public class TokenService
{
    private const int ExpirationMinutes = 30;

    private readonly JwtOptions _jwtOptions;
    private readonly byte[] _securityKey;

    public TokenService(IOptions<JwtOptions> options)
    {
        _jwtOptions = options.Value;
        _securityKey = Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey);
    }

    public string CreateToken(IdentityUser user)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var token = CreateJwtToken(CreateClaims(user), CreateSigningCredentials(), expiration);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private JwtSecurityToken CreateJwtToken(IEnumerable<Claim> claims, SigningCredentials credentials,
        DateTime expiration)
        =>
            new(_jwtOptions.Issuer, _jwtOptions.Audience, claims, expires: expiration,
                signingCredentials: credentials);

    private static IEnumerable<Claim> CreateClaims(IdentityUser user)
    {
        if (user.UserName == null || user.Email == null)
        {
            throw new ArgumentException("UserName or/and Email is null");
        }

        return new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, "TokenForTheApiWithAuth"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };
    }

    private SigningCredentials CreateSigningCredentials()
    {
        return new SigningCredentials(
            new SymmetricSecurityKey(_securityKey),
            SecurityAlgorithms.HmacSha256);
    }
}