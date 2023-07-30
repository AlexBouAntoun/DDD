// using Application.Queries;
// using MediatR;
// using Microsoft.EntityFrameworkCore;
// using Persistance;
// using Domain;
//
// namespace Application.Handlers;
//
// public class GetCoursesByTeacherHandler : IRequestHandler<GetCoursesByTeacherQuery, List<Course>>
// {
//     private readonly dbContext _dbContext;
//
//     public GetCoursesByTeacherHandler(dbContext dbContext)
//     {
//         _dbContext = dbContext;
//     }
//     public async Task<List<Course>> Handle(GetCoursesByTeacherQuery request, CancellationToken cancellationToken)
//     {
//         var teacher = _dbContext.Users.FirstOrDefault(x => x.Id == request.Id);
//         //var courses = _dbContext.TeacherPerCourses.FirstOrDefault(x => x.Id == request.Id);
//        // return Task.FromResult(_dbContext.TeacherPerCourses.ToListAsync())
//         //return Task.FromResult(_dbContext.Users.FirstOrDefault(x => x.Id == request.Id));
//         // _dbContext.
//         // User
//         // if (User.)
//         // {
//         //     
//         // }
//         // return await _dbContext.Courses.ToListAsync();
//     }
//
//    
// }