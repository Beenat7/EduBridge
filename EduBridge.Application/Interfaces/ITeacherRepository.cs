using EduBridge.Domain.Entities;

namespace EduBridge.Application.Interfaces;

public interface ITeacherRepository
{
    Task AddAsync(
        Teacher teacher,
        CancellationToken cancellationToken = default);

    Task<Teacher?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Teacher>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}