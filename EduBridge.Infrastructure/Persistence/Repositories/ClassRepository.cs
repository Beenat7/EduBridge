using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduBridge.Infrastructure.Persistence.Repositories;

public sealed class ClassRepository : IClassRepository
{
    private readonly EduBridgeDbContext _context;

    public ClassRepository(EduBridgeDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Class @class,
        CancellationToken cancellationToken = default)
    {
        await _context.Classes.AddAsync(
            @class,
            cancellationToken);
    }

    public async Task<Class?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Classes
            .FirstOrDefaultAsync(
                c => c.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Class>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Classes
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}