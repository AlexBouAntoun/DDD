using Domain;
using MediatR;

namespace Application.Queries;

public class GetCoursesQuery : IRequest<List<Course>>, IRequest<Course>
{
    public long Id { get; set; }
}