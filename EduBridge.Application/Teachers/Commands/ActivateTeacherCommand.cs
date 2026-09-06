using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Teachers.Commands;

public sealed record ActivateTeacherCommand(
    Guid Id)
    : IRequest<Teacher?>;

public sealed class ActivateTeacherCommandHandler
    : IRequestHandler<ActivateTeacherCommand, Teacher?>
{
    private readonly ITeacherRepository _teacherRepository;

    public ActivateTeacherCommandHandler(
        ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<Teacher?> Handle(
        ActivateTeacherCommand request,
        CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (teacher is null)
        {
            return null;
        }

        teacher.Activate();

        await _teacherRepository.SaveChangesAsync(
            cancellationToken);

        return teacher;
    }
}