using EduBridge.Domain.Schools;

namespace EduBridge.Application.Interfaces;

public interface ISchoolRepository
{
    Task AddAsync(
        School school,
        CancellationToken cancellationToken = default);

    Task<School?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<School?> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<School>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}