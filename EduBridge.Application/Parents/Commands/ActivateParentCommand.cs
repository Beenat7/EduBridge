using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Parents.Commands;

public sealed record ActivateParentCommand(
    Guid Id)
    : IRequest<Parent?>;

public sealed class ActivateParentCommandHandler
    : IRequestHandler<ActivateParentCommand, Parent?>
{
    private readonly IParentRepository _parentRepository;

    public ActivateParentCommandHandler(
        IParentRepository parentRepository)
    {
        _parentRepository = parentRepository;
    }

    public async Task<Parent?> Handle(
        ActivateParentCommand request,
        CancellationToken cancellationToken)
    {
        var parent = await _parentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (parent is null)
        {
            return null;
        }

        parent.Activate();

        await _parentRepository.SaveChangesAsync(
            cancellationToken);

        return parent;
    }
}