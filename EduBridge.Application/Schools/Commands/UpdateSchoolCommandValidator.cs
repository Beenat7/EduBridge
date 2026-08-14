using FluentValidation;

namespace EduBridge.Application.Schools.Commands.UpdateSchool;

public sealed class UpdateSchoolCommandValidator
    : AbstractValidator<UpdateSchoolCommand>
{
    public UpdateSchoolCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("School ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("School name is required.")
            .MaximumLength(200)
            .WithMessage("School name must not exceed 200 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("A valid email address is required.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .MaximumLength(20)
            .WithMessage("Phone number must not exceed 20 characters.");

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Address is required.")
            .MaximumLength(500)
            .WithMessage("Address must not exceed 500 characters.");
    }
}