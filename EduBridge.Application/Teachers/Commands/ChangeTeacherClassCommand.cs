using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace EduBridge.Application.Teachers.Commands;

public sealed record ChangeTeacherClassCommand(Guid TeacherId, Guid ClassId)
    : IRequest<Teacher?>;

public sealed class ChangeTeacherClassCommandHandler
    : IRequestHandler<ChangeTeacherClassCommand, Teacher?>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IClassRepository _classRepository;

    public ChangeTeacherClassCommandHandler(
        ITeacherRepository teacherRepository,
        IClassRepository classRepository)
    {
        _teacherRepository = teacherRepository;
        _classRepository = classRepository;
    }

    public async Task<Teacher?> Handle(
        ChangeTeacherClassCommand request,
        CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetByIdAsync(
            request.TeacherId,
            cancellationToken);

        if (teacher is null)
        {
            return null;
        }

        var @class = await _classRepository.GetByIdAsync(
            request.ClassId,
            cancellationToken);

        if (@class is null)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure(
                    nameof(request.ClassId),
                    "Class does not exist.")
            });
        }

        if (teacher.SchoolId != @class.SchoolId)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure(
                    nameof(request.ClassId),
                    "Teacher and class must belong to the same school.")
            });
        }

        teacher.ChangeClass(request.ClassId);

        await _teacherRepository.SaveChangesAsync(
            cancellationToken);

        return teacher;
    }
}
