using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduBridge.Infrastructure.Persistence.Repositories;

public sealed class TeacherSubjectRepository : ITeacherSubjectRepository
{
    private readonly EduBridgeDbContext _context;

    public TeacherSubjectRepository(EduBridgeDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        TeacherSubject teacherSubject,
        CancellationToken cancellationToken = default)
    {
        await _context.TeacherSubjects.AddAsync(
            teacherSubject,
            cancellationToken);
    }

    public async Task<TeacherSubject?> GetByTeacherAndSubjectAsync(
        Guid teacherId,
        Guid subjectId,
        CancellationToken cancellationToken = default)
    {
        return await _context.TeacherSubjects
            .FirstOrDefaultAsync(
                ts => ts.TeacherId == teacherId && ts.SubjectId == subjectId,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Subject>> GetSubjectsByTeacherIdAsync(
        Guid teacherId,
        CancellationToken cancellationToken = default)
    {
        return await _context.TeacherSubjects
            .Where(ts => ts.TeacherId == teacherId)
            .Join(
                _context.Subjects,
                ts => ts.SubjectId,
                subject => subject.Id,
                (_, subject) => subject)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Teacher>> GetTeachersBySubjectIdAsync(
        Guid subjectId,
        CancellationToken cancellationToken = default)
    {
        return await _context.TeacherSubjects
            .Where(ts => ts.SubjectId == subjectId)
            .Join(
                _context.Teachers,
                ts => ts.TeacherId,
                teacher => teacher.Id,
                (_, teacher) => teacher)
            .ToListAsync(cancellationToken);
    }

    public void Remove(TeacherSubject teacherSubject)
    {
        _context.TeacherSubjects.Remove(teacherSubject);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
