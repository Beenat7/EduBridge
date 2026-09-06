using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduBridge.Infrastructure.Persistence.Repositories;

public sealed class TeacherRepository : ITeacherRepository
{
    private readonly EduBridgeDbContext _context;

    public TeacherRepository(EduBridgeDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Teacher teacher,
        CancellationToken cancellationToken = default)
    {
        await _context.Teachers.AddAsync(
            teacher,
            cancellationToken);
    }

    public async Task<Teacher?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Teachers
            .FirstOrDefaultAsync(
                t => t.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Teacher>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Teachers
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}