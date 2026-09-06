using FluentValidation;

namespace EduBridge.Application.Teachers.Commands;

public sealed class UpdateTeacherCommandValidator
    : AbstractValidator<UpdateTeacherCommand>
{
    public UpdateTeacherCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Teacher ID is required.");

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

        RuleFor(x => x.EmployeeCode)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage(
                "Employee code is required and must not exceed 50 characters.");

        RuleFor(x => x.HireDate)
            .NotEmpty()
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage(
                "Hire date cannot be in the future.");
    }
}