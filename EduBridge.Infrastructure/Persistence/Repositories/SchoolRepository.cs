using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduBridge.Infrastructure.Persistence.Repositories;

public sealed class SchoolRepository : ISchoolRepository
{
    private readonly EduBridgeDbContext _context;

    public SchoolRepository(EduBridgeDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        School school,
        CancellationToken cancellationToken = default)
    {
        await _context.Schools.AddAsync(school, cancellationToken);
    }

    public async Task<School?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Schools
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<School?> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken = default)
    {
        return await _context.Schools
            .FirstOrDefaultAsync(s => s.Code == code, cancellationToken);
    }

    public async Task<IReadOnlyList<School>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Schools
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}