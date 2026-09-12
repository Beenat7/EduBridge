using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Announcements.Queries;

public sealed record GetAnnouncementByIdQuery(Guid Id)
    : IRequest<Announcement?>;

public sealed class GetAnnouncementByIdQueryHandler
    : IRequestHandler<GetAnnouncementByIdQuery, Announcement?>
{
    private readonly IAnnouncementRepository _announcementRepository;

    public GetAnnouncementByIdQueryHandler(
        IAnnouncementRepository announcementRepository)
    {
        _announcementRepository = announcementRepository;
    }

    public async Task<Announcement?> Handle(
        GetAnnouncementByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _announcementRepository.GetByIdAsync(
            request.Id,
            cancellationToken);
    }
}
