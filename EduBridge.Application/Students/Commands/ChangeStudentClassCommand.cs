using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace EduBridge.Application.Students.Commands;

public sealed record ChangeStudentClassCommand(Guid StudentId, Guid ClassId)
    : IRequest<Student?>;

public sealed class ChangeStudentClassCommandHandler
    : IRequestHandler<ChangeStudentClassCommand, Student?>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IClassRepository _classRepository;

    public ChangeStudentClassCommandHandler(
        IStudentRepository studentRepository,
        IClassRepository classRepository)
    {
        _studentRepository = studentRepository;
        _classRepository = classRepository;
    }

    public async Task<Student?> Handle(
        ChangeStudentClassCommand request,
        CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(
            request.StudentId,
            cancellationToken);

        if (student is null)
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

        if (student.SchoolId != @class.SchoolId)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure(
                    nameof(request.ClassId),
                    "Student and class must belong to the same school.")
            });
        }

        student.ChangeClass(request.ClassId);

        await _studentRepository.SaveChangesAsync(
            cancellationToken);

        return student;
    }
}
