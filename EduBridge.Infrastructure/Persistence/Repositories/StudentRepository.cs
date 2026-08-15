using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduBridge.Infrastructure.Persistence.Repositories;

public sealed class StudentRepository : IStudentRepository
{
    private readonly EduBridgeDbContext _context;

    public StudentRepository(EduBridgeDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Student student,
        CancellationToken cancellationToken = default)
    {
        await _context.Students.AddAsync(
            student,
            cancellationToken);
    }

    public async Task<Student?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Students
            .FirstOrDefaultAsync(
                s => s.Id == id,
                cancellationToken);
    }

    public async Task<Student?> GetByCodeAsync(
        string studentCode,
        CancellationToken cancellationToken = default)
    {
        return await _context.Students
            .FirstOrDefaultAsync(
                s => s.StudentCode == studentCode,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Student>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Students
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}