using FluentValidation;

namespace EduBridge.Application.DirectMessages.Commands;

public sealed class CreateDirectMessageCommandValidator
    : AbstractValidator<CreateDirectMessageCommand>
{
    public CreateDirectMessageCommandValidator()
    {
        RuleFor(x => x.SchoolId)
            .NotEmpty();

        RuleFor(x => x.StudentId)
            .NotEmpty();

        RuleFor(x => x.SenderId)
            .NotEmpty();

        RuleFor(x => x.RecipientId)
            .NotEmpty();

        RuleFor(x => x.Body)
            .NotEmpty()
            .MaximumLength(4000);

        RuleFor(x => x.SenderType)
            .IsInEnum();

        RuleFor(x => x.RecipientType)
            .IsInEnum();

        RuleFor(x => x)
            .Must(x => x.SenderType != x.RecipientType)
            .WithMessage("Sender and recipient must be different participants.");
    }
}
