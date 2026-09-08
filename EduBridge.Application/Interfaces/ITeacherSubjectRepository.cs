using EduBridge.Domain.Entities;

namespace EduBridge.Application.Interfaces;

public interface ITeacherSubjectRepository
{
    Task AddAsync(
        TeacherSubject teacherSubject,
        CancellationToken cancellationToken = default);

    Task<TeacherSubject?> GetByTeacherAndSubjectAsync(
        Guid teacherId,
        Guid subjectId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Subject>> GetSubjectsByTeacherIdAsync(
        Guid teacherId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Teacher>> GetTeachersBySubjectIdAsync(
        Guid subjectId,
        CancellationToken cancellationToken = default);

    void Remove(TeacherSubject teacherSubject);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}
