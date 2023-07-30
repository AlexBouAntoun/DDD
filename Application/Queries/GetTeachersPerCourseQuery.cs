using Domain;
using MediatR;

namespace Application.Queries;

public class GetTeachersPerCourseQuery : IRequest<List<TeacherPerCourse>>, IRequest<TeacherPerCourse>
{
public long Id { get; set; }
}