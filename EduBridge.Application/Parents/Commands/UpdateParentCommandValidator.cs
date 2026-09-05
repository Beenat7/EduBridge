using FluentValidation;

namespace EduBridge.Application.Parents.Commands;

public sealed class UpdateParentCommandValidator
    : AbstractValidator<UpdateParentCommand>
{
    public UpdateParentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Parent ID is required.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage(
                "First name is required and must not exceed 100 characters.");

        RuleFor(x => x.MiddleName)
            .MaximumLength(100)
            .WithMessage(
                "Middle name must not exceed 100 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage(
                "Last name is required and must not exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(255)
            .WithMessage(
                "A valid email address is required.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage(
                "Phone number is required and must not exceed 50 characters.");
    }
}