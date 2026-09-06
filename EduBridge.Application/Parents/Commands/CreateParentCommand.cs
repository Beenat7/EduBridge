using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Parents.Commands;

public sealed record CreateParentCommand(
    Guid SchoolId,
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber)
    : IRequest<Parent>;

public sealed class CreateParentCommandHandler
    : IRequestHandler<CreateParentCommand, Parent>
{
    private readonly IParentRepository _parentRepository;

    public CreateParentCommandHandler(
        IParentRepository parentRepository)
    {
        _parentRepository = parentRepository;
    }

    public async Task<Parent> Handle(
        CreateParentCommand request,
        CancellationToken cancellationToken)
    {
        var parent = new Parent(
            request.SchoolId,
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.Email,
            request.PhoneNumber);

        await _parentRepository.AddAsync(
            parent,
            cancellationToken);

        await _parentRepository.SaveChangesAsync(
            cancellationToken);

        return parent;
    }
}