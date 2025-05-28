using Microsoft.AspNetCore.Identity;
using SurveyBasket.Api.Authentication;
using System.Security.Cryptography;

namespace SurveyBasket.Api.Services;

public class AuthService 
   (UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly int _refreshTokenExpiryDays = 14;
    public async Task<AuthResponse?> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        //Check User?
        var user = await _userManager.FindByEmailAsync(email);
        if(user is null)
            return null;
        //Check Password?
         var isValidPassword =  await _userManager.CheckPasswordAsync(user, password);
         if(!isValidPassword)
            return null;
        //Generate JWT Token
        var (token, expiresIn) = _jwtProvider.GenerateToken(user);

        //Generate Refresh Token:
        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            ExpiresOn = refreshTokenExpiration,

        });
        await _userManager.UpdateAsync(user);
        //return new AuthResponse()
        return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName,token,expiresIn, refreshToken, refreshTokenExpiration);
    }
    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
