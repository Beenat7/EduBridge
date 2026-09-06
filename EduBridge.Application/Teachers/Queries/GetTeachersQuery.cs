using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Teachers.Queries;

public sealed record GetTeachersQuery
    : IRequest<IReadOnlyList<Teacher>>;

public sealed class GetTeachersQueryHandler
    : IRequestHandler<GetTeachersQuery, IReadOnlyList<Teacher>>
{
    private readonly ITeacherRepository _teacherRepository;

    public GetTeachersQueryHandler(
        ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<IReadOnlyList<Teacher>> Handle(
        GetTeachersQuery request,
        CancellationToken cancellationToken)
    {
        return await _teacherRepository.GetAllAsync(
            cancellationToken);
    }
}