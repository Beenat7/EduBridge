using FluentValidation;

namespace EduBridge.Application.Students.Commands;

public sealed class AssignStudentToClassCommandValidator
    : AbstractValidator<AssignStudentToClassCommand>
{
    public AssignStudentToClassCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty();

        RuleFor(x => x.ClassId)
            .NotEmpty();
    }
}
