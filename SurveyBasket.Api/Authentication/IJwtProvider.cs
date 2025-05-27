namespace SurveyBasket.Api.Authentication;

public interface IJwtProvider
{
    //Need one EndPoint => Return TwoValues(Token is a string, Time for Expire)

    (string token, int expiresIn)GenerateToken(ApplicationUser user);
    string? ValidateToken(string token); 

}
