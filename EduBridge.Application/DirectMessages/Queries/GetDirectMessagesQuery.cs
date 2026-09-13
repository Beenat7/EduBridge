using EduBridge.Application.Interfaces;
using EduBridge.Domain.Common.Enums;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.DirectMessages.Queries;

public sealed record GetDirectMessagesQuery(
    Guid? SchoolId = null,
    Guid? StudentId = null,
    Guid? ParticipantId = null,
    DirectMessageParticipantType? ParticipantType = null)
    : IRequest<IReadOnlyList<DirectMessage>>;

public sealed class GetDirectMessagesQueryHandler
    : IRequestHandler<GetDirectMessagesQuery, IReadOnlyList<DirectMessage>>
{
    private readonly IDirectMessageRepository _directMessageRepository;

    public GetDirectMessagesQueryHandler(
        IDirectMessageRepository directMessageRepository)
    {
        _directMessageRepository = directMessageRepository;
    }

    public async Task<IReadOnlyList<DirectMessage>> Handle(
        GetDirectMessagesQuery request,
        CancellationToken cancellationToken)
    {
        return await _directMessageRepository.GetAllAsync(
            request.SchoolId,
            request.StudentId,
            request.ParticipantId,
            request.ParticipantType,
            cancellationToken);
    }
}
