using FluentValidation;

namespace EduBridge.Application.Teachers.Commands;

public sealed class AssignTeacherToClassCommandValidator
    : AbstractValidator<AssignTeacherToClassCommand>
{
    public AssignTeacherToClassCommandValidator()
    {
        RuleFor(x => x.TeacherId)
            .NotEmpty();

        RuleFor(x => x.ClassId)
            .NotEmpty();
    }
}
