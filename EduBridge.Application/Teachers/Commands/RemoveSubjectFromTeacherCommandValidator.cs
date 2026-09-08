using FluentValidation;

namespace EduBridge.Application.Teachers.Commands;

public sealed class RemoveSubjectFromTeacherCommandValidator
    : AbstractValidator<RemoveSubjectFromTeacherCommand>
{
    public RemoveSubjectFromTeacherCommandValidator()
    {
        RuleFor(x => x.TeacherId)
            .NotEmpty();

        RuleFor(x => x.SubjectId)
            .NotEmpty();
    }
}
