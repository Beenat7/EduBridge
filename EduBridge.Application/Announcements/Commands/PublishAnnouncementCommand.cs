using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Announcements.Commands;

public sealed record PublishAnnouncementCommand(Guid Id)
    : IRequest<Announcement?>;

public sealed class PublishAnnouncementCommandHandler
    : IRequestHandler<PublishAnnouncementCommand, Announcement?>
{
    private readonly IAnnouncementRepository _announcementRepository;

    public PublishAnnouncementCommandHandler(
        IAnnouncementRepository announcementRepository)
    {
        _announcementRepository = announcementRepository;
    }

    public async Task<Announcement?> Handle(
        PublishAnnouncementCommand request,
        CancellationToken cancellationToken)
    {
        var announcement = await _announcementRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (announcement is null)
        {
            return null;
        }

        announcement.Publish();

        await _announcementRepository.SaveChangesAsync(
            cancellationToken);

        return announcement;
    }
}
