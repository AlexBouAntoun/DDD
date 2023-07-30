using Application.Queries;
using Application.Services;
using Domain;
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
    public async Task<IActionResult> GetAllCourses(int userId)
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
    
    
        // [Authorize]
     [HttpPost()]
     public ActionResult SaveCourse([FromBody] Course course)
     {
         var context = new dbContext();
    
         if (course.Id > 0)
         {
             var updatedCourse = context.Courses.FirstOrDefault(c => c.Id == course.Id);
    
             if (updatedCourse != null)
             {
                 updatedCourse.Name = course.Name;
                 updatedCourse.MaxStudentsNumber = course.MaxStudentsNumber;
                 updatedCourse.EnrolmentDateRange = course.EnrolmentDateRange;
    
                 context.Courses.Update(updatedCourse);
             }
         }
         else
         {
             context.Courses.Add(course);
         }
         
         context.SaveChanges();
         
         return Ok();
     }

     
    [HttpGet()]
    public ActionResult SearchCourseByName(string name)
    {
        var context = new dbContext();
        var courses = context.Courses.Where(w => w.Name != null && w.Name.ToLower().Contains(name.ToLower())).ToList();
    
        if (courses.Count == 0)
        {
            return Ok("Course not found");
        }
        return Ok(courses) ;
        
    }
    
    [HttpPost()]
    public ActionResult CreateSessionTime([FromBody] SessionTime sessionTime)
    {
        var context = new dbContext();
    
        TimeSpan span = sessionTime.EndTime - sessionTime.StartTime;
        sessionTime.Duration = Convert.ToInt32(span.TotalMinutes);
    
        sessionTime.EndTime = SetKindUtc(sessionTime.EndTime); 
        sessionTime.StartTime = SetKindUtc(sessionTime.StartTime);
        
        context.SessionTimes.Add(sessionTime);
        context.SaveChanges();
        
        return Ok();
    }
   
   [HttpPost()]
   public ActionResult RegisterCourse([FromBody] TeacherPerCourse teacherPerCourse)
   {
       var context = new dbContext();
   
       var newTeacherPerCourse = new TeacherPerCourse()
       {
           TeacherId = teacherPerCourse.TeacherId,
           CourseId = teacherPerCourse.CourseId
       };
           
       context.TeacherPerCourses.Add(newTeacherPerCourse);
       context.SaveChanges();
   
       foreach (var sessionTime in teacherPerCourse.TeacherPerCoursePerSessionTimes)
       {
           sessionTime.TeacherPerCourseId = teacherPerCourse.Id;
           context.TeacherPerCoursePerSessionTimes.Add(sessionTime);
       }
       
       context.SaveChanges();
       
       return Ok();
   }
   
   [HttpPost()]
   public ActionResult EnrollCourse([FromBody] ClassEnrollment classEnrollment)
   {
       var context = new dbContext();
       var teacherPerCourse = context.TeacherPerCourses.FirstOrDefault(c => c.Id == classEnrollment.ClassId);
       var course = context.Courses.FirstOrDefault(c => c.Id == teacherPerCourse.CourseId);
        
       
       var currentDate = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
       
       if ((course.EnrolmentDateRange != null) 
           && (currentDate < course.EnrolmentDateRange.Value.LowerBound || currentDate > course.EnrolmentDateRange.Value.UpperBound))
       {
           return Ok("Cannot enroll in course : Enrollment date passed");
       }
       else
       {
           if (course.MaxStudentsNumber != null)
           {
               var studentCount = context.ClassEnrollments.Where(c => c.ClassId == classEnrollment.ClassId).Count();
               if (course.MaxStudentsNumber == studentCount)
               {
                return Ok("Cannot enroll in course : Maximum number of students reached");
               }
           }
       }
       
       context.ClassEnrollments.Add(classEnrollment);
       context.SaveChanges();
       
       return Ok();
   }
   
   
    private static DateTime SetKindUtc(DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Utc) { return dateTime; }
        return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
    }

    // [HttpGet()]
    // public ActionResult GetAllCourses()
    // {
    //     var context = new dbContext();
    //
    //     var courses = context.Courses.ToList();
    //
    //     return Ok(courses);
    // }
    

}