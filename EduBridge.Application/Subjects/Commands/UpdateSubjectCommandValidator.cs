using FluentValidation;

namespace EduBridge.Application.Subjects.Commands;

public sealed class UpdateSubjectCommandValidator
    : AbstractValidator<UpdateSubjectCommand>
{
    public UpdateSubjectCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Subject ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage(
                "Subject name is required and must not exceed 200 characters.");

        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage(
                "Subject code is required and must not exceed 50 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage(
                "Subject description must not exceed 500 characters.");
    }
}