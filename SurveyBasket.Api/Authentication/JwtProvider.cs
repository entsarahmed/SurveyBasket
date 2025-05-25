using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyBasket.Api.Authentication;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly IOptions<JwtOptions> _options = options;

    public (string token, int expiresIn) GenerateToken(ApplicationUser user)
    {
        //you find Groups of Claims Add inside Token it
        //Claims (UserId, Email, Password, FirstName,LastName, Roles,Permission)
        Claim[] claims = [
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.GivenName,user.FirstName),
            new(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            ];
        //Generate Key Response That make encoding Token and DeCoding with Token
        var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
        var signingCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: _options.Value.Issuer,
            audience: _options.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.Value.ExpiryMinutes),
            signingCredentials: signingCredentials
            );
        return (token: new JwtSecurityTokenHandler().WriteToken(token), expiresIn: _options.Value.ExpiryMinutes * 60);
    }
}
