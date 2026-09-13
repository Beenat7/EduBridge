using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.DirectMessages.Queries;

public sealed record GetDirectMessageByIdQuery(Guid Id)
    : IRequest<DirectMessage?>;

public sealed class GetDirectMessageByIdQueryHandler
    : IRequestHandler<GetDirectMessageByIdQuery, DirectMessage?>
{
    private readonly IDirectMessageRepository _directMessageRepository;

    public GetDirectMessageByIdQueryHandler(
        IDirectMessageRepository directMessageRepository)
    {
        _directMessageRepository = directMessageRepository;
    }

    public async Task<DirectMessage?> Handle(
        GetDirectMessageByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _directMessageRepository.GetByIdAsync(
            request.Id,
            cancellationToken);
    }
}
