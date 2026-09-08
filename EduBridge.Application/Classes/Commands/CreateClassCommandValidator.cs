using FluentValidation;

namespace EduBridge.Application.Classes.Commands;

public sealed class CreateClassCommandValidator
    : AbstractValidator<CreateClassCommand>
{
    public CreateClassCommandValidator()
    {
        RuleFor(x => x.SchoolId)
            .NotEmpty()
            .WithMessage("School ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage(
                "Class name is required and must not exceed 200 characters.");

        RuleFor(x => x.GradeLevel)
            .IsInEnum()
            .WithMessage("Grade level must be valid.");

        RuleFor(x => x.Section)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage(
                "Class section is required and must not exceed 50 characters.");
    }
}