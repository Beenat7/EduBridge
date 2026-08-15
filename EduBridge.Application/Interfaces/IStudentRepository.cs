using EduBridge.Domain.Entities;

namespace EduBridge.Application.Interfaces;

public interface IStudentRepository
{
    Task AddAsync(
        Student student,
        CancellationToken cancellationToken = default);

    Task<Student?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Student?> GetByCodeAsync(
        string studentCode,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Student>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}