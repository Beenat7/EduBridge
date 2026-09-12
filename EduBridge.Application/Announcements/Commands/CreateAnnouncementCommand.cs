using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Announcements.Commands;

public sealed record CreateAnnouncementCommand(
    Guid SchoolId,
    string Title,
    string Body)
    : IRequest<Announcement>;

public sealed class CreateAnnouncementCommandHandler
    : IRequestHandler<CreateAnnouncementCommand, Announcement>
{
    private readonly IAnnouncementRepository _announcementRepository;

    public CreateAnnouncementCommandHandler(
        IAnnouncementRepository announcementRepository)
    {
        _announcementRepository = announcementRepository;
    }

    public async Task<Announcement> Handle(
        CreateAnnouncementCommand request,
        CancellationToken cancellationToken)
    {
        var announcement = new Announcement(
            request.SchoolId,
            request.Title,
            request.Body);

        await _announcementRepository.AddAsync(
            announcement,
            cancellationToken);

        await _announcementRepository.SaveChangesAsync(
            cancellationToken);

        return announcement;
    }
}
