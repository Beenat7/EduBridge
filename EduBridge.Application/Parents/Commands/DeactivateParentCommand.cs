using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Parents.Commands;

public sealed record DeactivateParentCommand(
    Guid Id)
    : IRequest<Parent?>;

public sealed class DeactivateParentCommandHandler
    : IRequestHandler<DeactivateParentCommand, Parent?>
{
    private readonly IParentRepository _parentRepository;

    public DeactivateParentCommandHandler(
        IParentRepository parentRepository)
    {
        _parentRepository = parentRepository;
    }

    public async Task<Parent?> Handle(
        DeactivateParentCommand request,
        CancellationToken cancellationToken)
    {
        var parent = await _parentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (parent is null)
        {
            return null;
        }

        parent.Deactivate();

        await _parentRepository.SaveChangesAsync(
            cancellationToken);

        return parent;
    }
}