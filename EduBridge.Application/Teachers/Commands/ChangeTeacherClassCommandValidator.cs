using FluentValidation;

namespace EduBridge.Application.Teachers.Commands;

public sealed class ChangeTeacherClassCommandValidator
    : AbstractValidator<ChangeTeacherClassCommand>
{
    public ChangeTeacherClassCommandValidator()
    {
        RuleFor(x => x.TeacherId)
            .NotEmpty();

        RuleFor(x => x.ClassId)
            .NotEmpty();
    }
}
