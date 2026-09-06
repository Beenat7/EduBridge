using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Teachers.Commands;

public sealed record DeactivateTeacherCommand(
    Guid Id)
    : IRequest<Teacher?>;

public sealed class DeactivateTeacherCommandHandler
    : IRequestHandler<DeactivateTeacherCommand, Teacher?>
{
    private readonly ITeacherRepository _teacherRepository;

    public DeactivateTeacherCommandHandler(
        ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<Teacher?> Handle(
        DeactivateTeacherCommand request,
        CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (teacher is null)
        {
            return null;
        }

        teacher.Deactivate();

        await _teacherRepository.SaveChangesAsync(
            cancellationToken);

        return teacher;
    }
}