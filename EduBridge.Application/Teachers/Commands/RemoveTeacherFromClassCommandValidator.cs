using FluentValidation;

namespace EduBridge.Application.Teachers.Commands;

public sealed class RemoveTeacherFromClassCommandValidator
    : AbstractValidator<RemoveTeacherFromClassCommand>
{
    public RemoveTeacherFromClassCommandValidator()
    {
        RuleFor(x => x.TeacherId)
            .NotEmpty();
    }
}
