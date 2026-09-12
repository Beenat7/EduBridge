using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Announcements.Queries;

public sealed record GetAnnouncementsQuery(Guid? SchoolId = null)
    : IRequest<IReadOnlyList<Announcement>>;

public sealed class GetAnnouncementsQueryHandler
    : IRequestHandler<GetAnnouncementsQuery, IReadOnlyList<Announcement>>
{
    private readonly IAnnouncementRepository _announcementRepository;

    public GetAnnouncementsQueryHandler(
        IAnnouncementRepository announcementRepository)
    {
        _announcementRepository = announcementRepository;
    }

    public async Task<IReadOnlyList<Announcement>> Handle(
        GetAnnouncementsQuery request,
        CancellationToken cancellationToken)
    {
        return await _announcementRepository.GetAllAsync(
            request.SchoolId,
            cancellationToken);
    }
}
