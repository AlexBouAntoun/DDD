using Application.Queries;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Persistance;

namespace Presentation.Controllers;

public class CoursesController : BaseController
{

    public CoursesController(IMediator mediator, IFirebaseAuthService authService) : base(mediator, authService)
    {
    }
    
    //[Authorize]
    //[AllowAnonymous]
    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> GetAll(int userId)
    {
        var context = new dbContext();
        var user = context.Users.FirstOrDefault(w => w.Id == userId);
        
        if (user == null)
        {
            return Ok("User Not Found");
        }
        else
        {
            if (user.RoleId == 1)//request header
            {
                var result = await Mediator.Send(new GetCoursesQuery());
                return Ok(result);
            }
            else
            {
                return Ok("Access Denied");
            }
        }
    }

}