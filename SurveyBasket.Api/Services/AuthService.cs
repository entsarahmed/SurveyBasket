using Microsoft.AspNetCore.Identity;

namespace SurveyBasket.Api.Services;

public class AuthService 
   (UserManager<ApplicationUser> userManager) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

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

            //return new AuthResponse()

            return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE0ODUxNDA5ODQsImlhdCI6MTQ4NTEzNzM4NCwiaXNzIjoiYWNtZS5jb20iLCJzdWIiOiIyOWFjMGMxOC0wYjRhLTQyY2YtODJmYy0wM2Q1NzAzMThhMWQiLCJhcHBsaWNhdGlvbklkIjoiNzkxMDM3MzQtOTdhYi00ZDFhLWFmMzctZTAwNmQwNWQyOTUyIiwicm9sZXMiOltdfQ.Mp0Pcwsz5VECK11Kf2ZZNF_SMKu5CgBeLN9ZOP04kZo", 3600);


    }
}
