using Application.Services;

//using Microsoft.AspNetCore.Mvc;

namespace Application.Models;

public class SignUpModel
{
    private readonly IFirebaseAuthService _authService;
    //private readonly IHttpContextAccessor _httpContextAccessor;

    //[BindProperty]
    //public SignUpUserDto UserDto { get; set; }

    public SignUpModel(IFirebaseAuthService authService )
    {
        _authService = authService;
    }
    

    public async Task<string?> OnPostAsync(string userEmail, string userPassword)
    {
        try
        {
            var token = await _authService.SignUp(userEmail, userPassword);
            return token;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}

