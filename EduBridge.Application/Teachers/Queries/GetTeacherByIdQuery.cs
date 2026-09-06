using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Teachers.Queries;

public sealed record GetTeacherByIdQuery(
    Guid Id)
    : IRequest<Teacher?>;

public sealed class GetTeacherByIdQueryHandler
    : IRequestHandler<GetTeacherByIdQuery, Teacher?>
{
    private readonly ITeacherRepository _teacherRepository;

    public GetTeacherByIdQueryHandler(
        ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<Teacher?> Handle(
        GetTeacherByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _teacherRepository.GetByIdAsync(
            request.Id,
            cancellationToken);
    }
}