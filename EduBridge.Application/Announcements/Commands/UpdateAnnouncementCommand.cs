using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Announcements.Commands;

public sealed record UpdateAnnouncementCommand(
    Guid Id,
    string Title,
    string Body)
    : IRequest<Announcement?>;

public sealed class UpdateAnnouncementCommandHandler
    : IRequestHandler<UpdateAnnouncementCommand, Announcement?>
{
    private readonly IAnnouncementRepository _announcementRepository;

    public UpdateAnnouncementCommandHandler(
        IAnnouncementRepository announcementRepository)
    {
        _announcementRepository = announcementRepository;
    }

    public async Task<Announcement?> Handle(
        UpdateAnnouncementCommand request,
        CancellationToken cancellationToken)
    {
        var announcement = await _announcementRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (announcement is null)
        {
            return null;
        }

        announcement.Update(
            request.Title,
            request.Body);

        await _announcementRepository.SaveChangesAsync(
            cancellationToken);

        return announcement;
    }
}
