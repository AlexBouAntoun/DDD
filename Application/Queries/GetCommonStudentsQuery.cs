using Domain;
using MediatR;

namespace Application.Queries;

public class GetCommonStudentsQuery : IRequest<List<Course>>, IRequest<Course>
{
    public long Id { get; set; }
}
