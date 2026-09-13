using EduBridge.Application.Interfaces;
using EduBridge.Domain.Common.Enums;
using EduBridge.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace EduBridge.Application.Students.Commands;

public sealed record AssignStudentToParentCommand(Guid StudentId, Guid ParentId)
    : IRequest<Student?>;

public sealed class AssignStudentToParentCommandHandler
    : IRequestHandler<AssignStudentToParentCommand, Student?>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IParentRepository _parentRepository;

    public AssignStudentToParentCommandHandler(
        IStudentRepository studentRepository,
        IParentRepository parentRepository)
    {
        _studentRepository = studentRepository;
        _parentRepository = parentRepository;
    }

    public async Task<Student?> Handle(
        AssignStudentToParentCommand request,
        CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(
            request.StudentId,
            cancellationToken);

        if (student is null)
        {
            return null;
        }

        var parent = await _parentRepository.GetByIdAsync(
            request.ParentId,
            cancellationToken);

        if (parent is null)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure(
                    nameof(request.ParentId),
                    "Parent does not exist.")
            });
        }

        if (student.SchoolId != parent.SchoolId)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure(
                    nameof(request.ParentId),
                    "Student and parent must belong to the same school.")
            });
        }

        if (parent.Status == ParentStatus.Archived)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure(
                    nameof(request.ParentId),
                    "Archived parents cannot be assigned to students.")
            });
        }

        if (parent.Status != ParentStatus.Active)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure(
                    nameof(request.ParentId),
                    "Parent must be active to be assigned to a student.")
            });
        }

        student.AssignParent(request.ParentId);

        await _studentRepository.SaveChangesAsync(
            cancellationToken);

        return student;
    }
}
