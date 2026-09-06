using EduBridge.Domain.Entities;

namespace EduBridge.Application.Interfaces;

public interface IParentRepository
{
    Task AddAsync(
        Parent parent,
        CancellationToken cancellationToken = default);

    Task<Parent?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Parent>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}