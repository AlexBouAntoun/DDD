using System.Collections;
using Application.Models;
using Application.Queries;
using Application.Services;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Persistance;

namespace Presentation.Controllers;

public class UserController : BaseController
{
    private readonly IFirebaseAuthService authService;

    public UserController(IMediator mediator, IFirebaseAuthService authService) : base(mediator, authService)
    {
        this.authService = authService;
    }
    
     [EnableQuery]
     [HttpGet()]
     public IActionResult GetUserByID([FromODataUri] int userId) //OdataExample
     {
         var context = new dbContext();
         var user = context.Users.FirstOrDefault(w => w.Id == userId);
     
         return Ok(user);
     }
     
     [HttpGet]
     public async Task<IActionResult> SignUp(String Email, String Password)
     {
         
         var signUp = new SignUpModel(authService);
         var token = signUp.OnPostAsync(Email, Password);

         if (token == null)
         {
             return Ok("An error has occured while generating the token");
         }
         else
         {
             return Ok("success");
         }
     }

     // [HttpGet]
     // public async Task<List<string>> GetCommonStudents(int teacherId1, int teacherId2)
     // {
     //     
     //     var teacher1 = await Mediator.Send(new GetTeacherByIdQuery(teacherId1));
     //     var teacher2 = await Mediator.Send(new GetTeacherByIdQuery(teacherId2));
     //     if (teacher1.RoleId != 2 || teacher2.RoleId != 2)
     //     {
     //         return new List<string> { ("one or both users are not a teacher") };
     //     }
     //
     //     var teacher1CoursesId = new List<int>{};
     //     var teacher2CoursesId = new List<int>{};
     //     //var Courses = await Mediator.Send(new GetCoursesQuery());
     //     foreach (var course in (IEnumerable)typeof(TeacherPerCourse))
     //     {
     //         if (teacher1.Id == TeacherPerCourse.TeacherId )
     //         {
     //             teacher1CoursesId.add= TeacherPerCourse.CourseId;
     //         }
     //         if (teacher2.Id == TeacherPerCourse.TeacherId )
     //         {
     //             teacher2CoursesId = TeacherPerCourse.CourseId;
     //         }
     //     }
     //     
     //     
     // }
     
}