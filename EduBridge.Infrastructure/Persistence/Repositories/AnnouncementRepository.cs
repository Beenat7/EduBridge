using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduBridge.Infrastructure.Persistence.Repositories;

public sealed class AnnouncementRepository : IAnnouncementRepository
{
    private readonly EduBridgeDbContext _context;

    public AnnouncementRepository(EduBridgeDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Announcement announcement,
        CancellationToken cancellationToken = default)
    {
        await _context.Announcements.AddAsync(
            announcement,
            cancellationToken);
    }

    public async Task<Announcement?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Announcements
            .FirstOrDefaultAsync(
                a => a.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Announcement>> GetAllAsync(
        Guid? schoolId = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Announcements.AsQueryable();

        if (schoolId.HasValue)
        {
            query = query.Where(a => a.SchoolId == schoolId.Value);
        }

        return await query
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
