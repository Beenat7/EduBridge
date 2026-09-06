using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Parents.Commands;

public sealed record UpdateParentCommand(
    Guid Id,
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber)
    : IRequest<Parent?>;

public sealed class UpdateParentCommandHandler
    : IRequestHandler<UpdateParentCommand, Parent?>
{
    private readonly IParentRepository _parentRepository;

    public UpdateParentCommandHandler(
        IParentRepository parentRepository)
    {
        _parentRepository = parentRepository;
    }

    public async Task<Parent?> Handle(
        UpdateParentCommand request,
        CancellationToken cancellationToken)
    {
        var parent = await _parentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (parent is null)
        {
            return null;
        }

        parent.Update(
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.Email,
            request.PhoneNumber);

        await _parentRepository.SaveChangesAsync(
            cancellationToken);

        return parent;
    }
}