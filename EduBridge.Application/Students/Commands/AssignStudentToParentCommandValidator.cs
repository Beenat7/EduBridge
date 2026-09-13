using FluentValidation;

namespace EduBridge.Application.Students.Commands;

public sealed class AssignStudentToParentCommandValidator
    : AbstractValidator<AssignStudentToParentCommand>
{
    public AssignStudentToParentCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty();

        RuleFor(x => x.ParentId)
            .NotEmpty();
    }
}
