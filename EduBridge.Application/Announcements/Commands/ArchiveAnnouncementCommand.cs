using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Announcements.Commands;

public sealed record ArchiveAnnouncementCommand(Guid Id)
    : IRequest<Announcement?>;

public sealed class ArchiveAnnouncementCommandHandler
    : IRequestHandler<ArchiveAnnouncementCommand, Announcement?>
{
    private readonly IAnnouncementRepository _announcementRepository;

    public ArchiveAnnouncementCommandHandler(
        IAnnouncementRepository announcementRepository)
    {
        _announcementRepository = announcementRepository;
    }

    public async Task<Announcement?> Handle(
        ArchiveAnnouncementCommand request,
        CancellationToken cancellationToken)
    {
        var announcement = await _announcementRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (announcement is null)
        {
            return null;
        }

        announcement.Archive();

        await _announcementRepository.SaveChangesAsync(
            cancellationToken);

        return announcement;
    }
}
