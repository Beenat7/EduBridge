using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Parents.Commands;

public sealed record ArchiveParentCommand(
    Guid Id)
    : IRequest<Parent?>;

public sealed class ArchiveParentCommandHandler
    : IRequestHandler<ArchiveParentCommand, Parent?>
{
    private readonly IParentRepository _parentRepository;

    public ArchiveParentCommandHandler(
        IParentRepository parentRepository)
    {
        _parentRepository = parentRepository;
    }

    public async Task<Parent?> Handle(
        ArchiveParentCommand request,
        CancellationToken cancellationToken)
    {
        var parent = await _parentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (parent is null)
        {
            return null;
        }

        parent.Archive();

        await _parentRepository.SaveChangesAsync(
            cancellationToken);

        return parent;
    }
}