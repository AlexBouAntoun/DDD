using Domain;
using MediatR;

namespace Application.Queries;

public class GetCoursesByTeacherQuery : IRequest<List<Course>>, IRequest<Course>
{
    public long Id { get; set; }
}