using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;


[ApiController]
[Route("api/[controller]")]
public class BaseController: ControllerBase
{
    protected readonly IMediator Mediator;
    protected readonly IFirebaseAuthService AuthService;
    public BaseController(IMediator mediator, IFirebaseAuthService authService)
    {
        Mediator = mediator;
        AuthService = authService;
    }
    
}

