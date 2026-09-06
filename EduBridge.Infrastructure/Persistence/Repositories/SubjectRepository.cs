using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduBridge.Infrastructure.Persistence.Repositories;

public sealed class SubjectRepository : ISubjectRepository
{
    private readonly EduBridgeDbContext _context;

    public SubjectRepository(EduBridgeDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Subject subject,
        CancellationToken cancellationToken = default)
    {
        await _context.Subjects.AddAsync(
            subject,
            cancellationToken);
    }

    public async Task<Subject?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Subjects
            .FirstOrDefaultAsync(
                s => s.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Subject>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Subjects
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}