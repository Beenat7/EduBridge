using EduBridge.Domain.Entities;

namespace EduBridge.Application.Interfaces;

public interface IClassSubjectRepository
{
    Task AddAsync(
        ClassSubject classSubject,
        CancellationToken cancellationToken = default);

    Task<ClassSubject?> GetByClassAndSubjectAsync(
        Guid classId,
        Guid subjectId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Subject>> GetSubjectsByClassIdAsync(
        Guid classId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Class>> GetClassesBySubjectIdAsync(
        Guid subjectId,
        CancellationToken cancellationToken = default);

    void Remove(ClassSubject classSubject);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}
