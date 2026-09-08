using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduBridge.Infrastructure.Persistence.Repositories;

public sealed class ClassSubjectRepository : IClassSubjectRepository
{
    private readonly EduBridgeDbContext _context;

    public ClassSubjectRepository(EduBridgeDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        ClassSubject classSubject,
        CancellationToken cancellationToken = default)
    {
        await _context.ClassSubjects.AddAsync(
            classSubject,
            cancellationToken);
    }

    public async Task<ClassSubject?> GetByClassAndSubjectAsync(
        Guid classId,
        Guid subjectId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ClassSubjects
            .FirstOrDefaultAsync(
                cs => cs.ClassId == classId && cs.SubjectId == subjectId,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Subject>> GetSubjectsByClassIdAsync(
        Guid classId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ClassSubjects
            .Where(cs => cs.ClassId == classId)
            .Join(
                _context.Subjects,
                cs => cs.SubjectId,
                subject => subject.Id,
                (_, subject) => subject)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Class>> GetClassesBySubjectIdAsync(
        Guid subjectId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ClassSubjects
            .Where(cs => cs.SubjectId == subjectId)
            .Join(
                _context.Classes,
                cs => cs.ClassId,
                @class => @class.Id,
                (_, @class) => @class)
            .ToListAsync(cancellationToken);
    }

    public void Remove(ClassSubject classSubject)
    {
        _context.ClassSubjects.Remove(classSubject);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
