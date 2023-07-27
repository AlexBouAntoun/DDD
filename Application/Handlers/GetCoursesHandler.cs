using Application.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Domain;

namespace Application.Handlers;

public class GetCoursesHandler : IRequestHandler<GetCoursesQuery, List<Course>>
{
    private readonly dbContext _dbContext;

    public GetCoursesHandler(dbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Course>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Courses.ToListAsync();
    }
}

// public class GetCoursesHandler : IRequestHandler<GetCoursesQuery, IEnumerable<Course>>
// {
//     private readonly dbContext _dbContext;
//
//     public GetCoursesHandler(dbContext dbContext) => _dbContext = dbContext;
//     public async Task<IEnumerable<Course>> Handle(GetCoursesQuery request, CancellationToken cancellationToken) => 
//         await _dbContext.Courses.ToListAsync();
// }