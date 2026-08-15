using EduBridge.Domain.Common.Enums;
using FluentValidation;

namespace EduBridge.Application.Students.Commands;

public sealed class UpdateStudentCommandValidator
    : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.MiddleName)
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .LessThan(DateTime.UtcNow);

        RuleFor(x => x.Gender)
            .NotEmpty()
            .Must(BeValidGender)
            .WithMessage(
                "Gender must be Male or Female.");

        RuleFor(x => x.Grade)
            .NotEmpty()
            .MaximumLength(50);
    }

    private static bool BeValidGender(string gender)
    {
        return Enum.TryParse<Gender>(
            gender,
            ignoreCase: true,
            out _);
    }
}