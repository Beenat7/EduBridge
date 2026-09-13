using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.DirectMessages.Commands;

public sealed record MarkDirectMessageAsReadCommand(Guid Id)
    : IRequest<DirectMessage?>;

public sealed class MarkDirectMessageAsReadCommandHandler
    : IRequestHandler<MarkDirectMessageAsReadCommand, DirectMessage?>
{
    private readonly IDirectMessageRepository _directMessageRepository;

    public MarkDirectMessageAsReadCommandHandler(
        IDirectMessageRepository directMessageRepository)
    {
        _directMessageRepository = directMessageRepository;
    }

    public async Task<DirectMessage?> Handle(
        MarkDirectMessageAsReadCommand request,
        CancellationToken cancellationToken)
    {
        var directMessage = await _directMessageRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (directMessage is null)
        {
            return null;
        }

        directMessage.MarkAsRead();

        await _directMessageRepository.SaveChangesAsync(
            cancellationToken);

        return directMessage;
    }
}
