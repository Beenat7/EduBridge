using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Teachers.Commands;

public sealed record RemoveTeacherFromClassCommand(Guid TeacherId)
    : IRequest<Teacher?>;

public sealed class RemoveTeacherFromClassCommandHandler
    : IRequestHandler<RemoveTeacherFromClassCommand, Teacher?>
{
    private readonly ITeacherRepository _teacherRepository;

    public RemoveTeacherFromClassCommandHandler(
        ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<Teacher?> Handle(
        RemoveTeacherFromClassCommand request,
        CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetByIdAsync(
            request.TeacherId,
            cancellationToken);

        if (teacher is null)
        {
            return null;
        }

        teacher.RemoveFromClass();

        await _teacherRepository.SaveChangesAsync(
            cancellationToken);

        return teacher;
    }
}
