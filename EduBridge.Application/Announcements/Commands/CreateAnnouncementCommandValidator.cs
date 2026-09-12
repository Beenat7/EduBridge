using FluentValidation;

namespace EduBridge.Application.Announcements.Commands;

public sealed class CreateAnnouncementCommandValidator
    : AbstractValidator<CreateAnnouncementCommand>
{
    public CreateAnnouncementCommandValidator()
    {
        RuleFor(x => x.SchoolId)
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Body)
            .NotEmpty()
            .MaximumLength(4000);
    }
}
