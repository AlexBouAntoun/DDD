using Application.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Domain;

namespace Application.Handlers;

public class GetCommonStudentsHandlers : IRequestHandler<GetCoursesQuery, List<Course>>
{
    private readonly dbContext _dbContext;

    public GetCommonStudentsHandlers(dbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Course>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Courses.ToListAsync();
    }
}