using Domain;
using MediatR;

namespace Application.Queries;

public class GetTeacherByIdQuery : IRequest<User>
{
    public GetTeacherByIdQuery(int Id)
    {
        throw new NotImplementedException();
    }

    public long Id { get; set; }
    public int RoleId { get; set; }
}