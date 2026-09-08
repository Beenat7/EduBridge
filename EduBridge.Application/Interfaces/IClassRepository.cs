using EduBridge.Domain.Entities;

namespace EduBridge.Application.Interfaces;

public interface IClassRepository
{
    Task AddAsync(
        Class @class,
        CancellationToken cancellationToken = default);

    Task<Class?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Class>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}