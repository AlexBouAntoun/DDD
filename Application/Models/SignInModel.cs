using Application.Services;

namespace Application.Models;

public class SignInModel
{
    private readonly IFirebaseAuthService _authService;
   

    public SignInModel(IFirebaseAuthService authService )
    {
        _authService = authService;
    }
    
    public async Task<string?> OnPostAsync(string userEmail, string userPassword)
    {
        try
        {
            var token = await _authService.Login(userEmail, userPassword);
            return token;
        }
        catch (Exception e)
        {
            return null;
        }
    }
    
}
