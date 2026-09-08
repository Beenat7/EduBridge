using FluentValidation;

namespace EduBridge.Application.Teachers.Commands;

public sealed class AssignSubjectToTeacherCommandValidator
    : AbstractValidator<AssignSubjectToTeacherCommand>
{
    public AssignSubjectToTeacherCommandValidator()
    {
        RuleFor(x => x.TeacherId)
            .NotEmpty();

        RuleFor(x => x.SubjectId)
            .NotEmpty();
    }
}
