using FluentValidation;

namespace EduBridge.Application.Students.Commands;

public sealed class RemoveStudentFromClassCommandValidator
    : AbstractValidator<RemoveStudentFromClassCommand>
{
    public RemoveStudentFromClassCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty();
    }
}
