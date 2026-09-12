using EduBridge.Domain.Entities;

namespace EduBridge.Application.Interfaces;

public interface IAnnouncementRepository
{
    Task AddAsync(
        Announcement announcement,
        CancellationToken cancellationToken = default);

    Task<Announcement?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Announcement>> GetAllAsync(
        Guid? schoolId = null,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}
