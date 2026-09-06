using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Parents.Queries;

public sealed record GetParentByIdQuery(
    Guid Id)
    : IRequest<Parent?>;

public sealed class GetParentByIdQueryHandler
    : IRequestHandler<GetParentByIdQuery, Parent?>
{
    private readonly IParentRepository _parentRepository;

    public GetParentByIdQueryHandler(
        IParentRepository parentRepository)
    {
        _parentRepository = parentRepository;
    }

    public async Task<Parent?> Handle(
        GetParentByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _parentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);
    }
}