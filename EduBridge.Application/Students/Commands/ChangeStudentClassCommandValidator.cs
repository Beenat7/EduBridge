using FluentValidation;

namespace EduBridge.Application.Students.Commands;

public sealed class ChangeStudentClassCommandValidator
    : AbstractValidator<ChangeStudentClassCommand>
{
    public ChangeStudentClassCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty();

        RuleFor(x => x.ClassId)
            .NotEmpty();
    }
}
