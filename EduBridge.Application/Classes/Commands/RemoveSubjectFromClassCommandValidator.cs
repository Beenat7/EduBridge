using FluentValidation;

namespace EduBridge.Application.Classes.Commands;

public sealed class RemoveSubjectFromClassCommandValidator
    : AbstractValidator<RemoveSubjectFromClassCommand>
{
    public RemoveSubjectFromClassCommandValidator()
    {
        RuleFor(x => x.ClassId)
            .NotEmpty();

        RuleFor(x => x.SubjectId)
            .NotEmpty();
    }
}
