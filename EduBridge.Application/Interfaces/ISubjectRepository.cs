using EduBridge.Domain.Entities;

namespace EduBridge.Application.Interfaces;

public interface ISubjectRepository
{
    Task AddAsync(
        Subject subject,
        CancellationToken cancellationToken = default);

    Task<Subject?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Subject>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}