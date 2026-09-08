using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Subjects.Queries;

public sealed record GetSubjectTeachersQuery(Guid SubjectId)
    : IRequest<IReadOnlyList<Teacher>?>;

public sealed class GetSubjectTeachersQueryHandler
    : IRequestHandler<GetSubjectTeachersQuery, IReadOnlyList<Teacher>?>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly ITeacherSubjectRepository _teacherSubjectRepository;

    public GetSubjectTeachersQueryHandler(
        ISubjectRepository subjectRepository,
        ITeacherSubjectRepository teacherSubjectRepository)
    {
        _subjectRepository = subjectRepository;
        _teacherSubjectRepository = teacherSubjectRepository;
    }

    public async Task<IReadOnlyList<Teacher>?> Handle(
        GetSubjectTeachersQuery request,
        CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.GetByIdAsync(
            request.SubjectId,
            cancellationToken);

        if (subject is null)
        {
            return null;
        }

        return await _teacherSubjectRepository.GetTeachersBySubjectIdAsync(
            request.SubjectId,
            cancellationToken);
    }
}
