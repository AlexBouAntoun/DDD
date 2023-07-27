// using Application.Models;
// using Application.Services;
// using MediatR;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.OData.Formatter;
// using Microsoft.AspNetCore.OData.Query;
// using Persistance;
//
// namespace Presentation.Controllers;
//
// public class UserController : BaseController
// {
//     private readonly IFirebaseAuthService authService;
//
//     public UserController(IMediator mediator, IFirebaseAuthService authService) : base(mediator, authService)
//     {
//         this.authService = authService;
//     }
//     
//      [EnableQuery]
//      [HttpGet()]
//      public IActionResult GetUserByID([FromODataUri] int userId)
//      {
//          var context = new dbContext();
//          var user = context.Users.FirstOrDefault(w => w.Id == userId);
//
//          return Ok(user);
//      }
//      
//      [HttpGet]
//      public async Task<IActionResult> SignUp(String Email, String Password)
//      {
//          
//          var signUp = new SignUpModel(authService);
//          var token = signUp.OnPostAsync(Email, Password);
//
//          if (token == null)
//          {
//              return Ok("An error has occured while generating the token");
//          }
//          else
//          {
//              return Ok("success");
//          }
//      }
//
//      
// }