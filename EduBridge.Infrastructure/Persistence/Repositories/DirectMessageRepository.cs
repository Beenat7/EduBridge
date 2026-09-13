using EduBridge.Application.Interfaces;
using EduBridge.Domain.Common.Enums;
using EduBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduBridge.Infrastructure.Persistence.Repositories;

public sealed class DirectMessageRepository : IDirectMessageRepository
{
    private readonly EduBridgeDbContext _context;

    public DirectMessageRepository(EduBridgeDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        DirectMessage directMessage,
        CancellationToken cancellationToken = default)
    {
        await _context.DirectMessages.AddAsync(
            directMessage,
            cancellationToken);
    }

    public async Task<DirectMessage?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.DirectMessages
            .FirstOrDefaultAsync(
                d => d.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<DirectMessage>> GetAllAsync(
        Guid? schoolId = null,
        Guid? studentId = null,
        Guid? participantId = null,
        DirectMessageParticipantType? participantType = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.DirectMessages.AsQueryable();

        if (schoolId.HasValue)
        {
            query = query.Where(d => d.SchoolId == schoolId.Value);
        }

        if (studentId.HasValue)
        {
            query = query.Where(d => d.StudentId == studentId.Value);
        }

        if (participantId.HasValue)
        {
            query = query.Where(d =>
                d.SenderId == participantId.Value ||
                d.RecipientId == participantId.Value);
        }

        if (participantType.HasValue)
        {
            query = query.Where(d =>
                d.SenderType == participantType.Value ||
                d.RecipientType == participantType.Value);
        }

        return await query
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
