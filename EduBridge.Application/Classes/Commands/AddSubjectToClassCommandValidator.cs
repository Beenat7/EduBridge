using FluentValidation;

namespace EduBridge.Application.Classes.Commands;

public sealed class AddSubjectToClassCommandValidator
    : AbstractValidator<AddSubjectToClassCommand>
{
    public AddSubjectToClassCommandValidator()
    {
        RuleFor(x => x.ClassId)
            .NotEmpty();

        RuleFor(x => x.SubjectId)
            .NotEmpty();
    }
}
