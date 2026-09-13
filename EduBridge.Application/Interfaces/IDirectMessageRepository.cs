using EduBridge.Domain.Common.Enums;
using EduBridge.Domain.Entities;

namespace EduBridge.Application.Interfaces;

public interface IDirectMessageRepository
{
    Task AddAsync(
        DirectMessage directMessage,
        CancellationToken cancellationToken = default);

    Task<DirectMessage?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DirectMessage>> GetAllAsync(
        Guid? schoolId = null,
        Guid? studentId = null,
        Guid? participantId = null,
        DirectMessageParticipantType? participantType = null,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}
