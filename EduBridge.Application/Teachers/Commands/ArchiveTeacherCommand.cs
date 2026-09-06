using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Teachers.Commands;

public sealed record ArchiveTeacherCommand(
    Guid Id)
    : IRequest<Teacher?>;

public sealed class ArchiveTeacherCommandHandler
    : IRequestHandler<ArchiveTeacherCommand, Teacher?>
{
    private readonly ITeacherRepository _teacherRepository;

    public ArchiveTeacherCommandHandler(
        ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<Teacher?> Handle(
        ArchiveTeacherCommand request,
        CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (teacher is null)
        {
            return null;
        }

        teacher.Archive();

        await _teacherRepository.SaveChangesAsync(
            cancellationToken);

        return teacher;
    }
}