using Application.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Domain;

namespace Application.Handlers;

public class GetTeacherByIdHandler : IRequestHandler<GetTeacherByIdQuery, User>
{
    private readonly dbContext _dbContext;

    public GetTeacherByIdHandler(dbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<User> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
    {
        //return await _dbContext.Courses.ToListAsync();
        
        return Task.FromResult(_dbContext.Users.FirstOrDefault(x => x.Id == request.Id));
        //ToListAsync().Id.Equals()
        //.FirstOrDefault(x => x.Id == request.Id);
        //GetAll().FirstOrDefault(x => x.Id == request.Id);
    }
    
}