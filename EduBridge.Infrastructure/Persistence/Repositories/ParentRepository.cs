using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduBridge.Infrastructure.Persistence.Repositories;

public sealed class ParentRepository : IParentRepository
{
    private readonly EduBridgeDbContext _context;

    public ParentRepository(EduBridgeDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Parent parent,
        CancellationToken cancellationToken = default)
    {
        await _context.Parents.AddAsync(
            parent,
            cancellationToken);
    }

    public async Task<Parent?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Parents
            .FirstOrDefaultAsync(
                p => p.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Parent>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Parents
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}