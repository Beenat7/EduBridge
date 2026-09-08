using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Teachers.Commands;

public sealed record RemoveSubjectFromTeacherCommand(Guid TeacherId, Guid SubjectId)
    : IRequest<TeacherSubject?>;

public sealed class RemoveSubjectFromTeacherCommandHandler
    : IRequestHandler<RemoveSubjectFromTeacherCommand, TeacherSubject?>
{
    private readonly ITeacherSubjectRepository _teacherSubjectRepository;

    public RemoveSubjectFromTeacherCommandHandler(
        ITeacherSubjectRepository teacherSubjectRepository)
    {
        _teacherSubjectRepository = teacherSubjectRepository;
    }

    public async Task<TeacherSubject?> Handle(
        RemoveSubjectFromTeacherCommand request,
        CancellationToken cancellationToken)
    {
        var teacherSubject = await _teacherSubjectRepository
            .GetByTeacherAndSubjectAsync(
                request.TeacherId,
                request.SubjectId,
                cancellationToken);

        if (teacherSubject is null)
        {
            return null;
        }

        _teacherSubjectRepository.Remove(teacherSubject);

        await _teacherSubjectRepository.SaveChangesAsync(
            cancellationToken);

        return teacherSubject;
    }
}
