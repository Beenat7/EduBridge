using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Teachers.Queries;

public sealed record GetTeacherSubjectsQuery(Guid TeacherId)
    : IRequest<IReadOnlyList<Subject>?>;

public sealed class GetTeacherSubjectsQueryHandler
    : IRequestHandler<GetTeacherSubjectsQuery, IReadOnlyList<Subject>?>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ITeacherSubjectRepository _teacherSubjectRepository;

    public GetTeacherSubjectsQueryHandler(
        ITeacherRepository teacherRepository,
        ITeacherSubjectRepository teacherSubjectRepository)
    {
        _teacherRepository = teacherRepository;
        _teacherSubjectRepository = teacherSubjectRepository;
    }

    public async Task<IReadOnlyList<Subject>?> Handle(
        GetTeacherSubjectsQuery request,
        CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetByIdAsync(
            request.TeacherId,
            cancellationToken);

        if (teacher is null)
        {
            return null;
        }

        return await _teacherSubjectRepository.GetSubjectsByTeacherIdAsync(
            request.TeacherId,
            cancellationToken);
    }
}
