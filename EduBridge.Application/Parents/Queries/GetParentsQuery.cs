using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Parents.Queries;

public sealed record GetParentsQuery
    : IRequest<IReadOnlyList<Parent>>;

public sealed class GetParentsQueryHandler
    : IRequestHandler<GetParentsQuery, IReadOnlyList<Parent>>
{
    private readonly IParentRepository _parentRepository;

    public GetParentsQueryHandler(
        IParentRepository parentRepository)
    {
        _parentRepository = parentRepository;
    }

    public async Task<IReadOnlyList<Parent>> Handle(
        GetParentsQuery request,
        CancellationToken cancellationToken)
    {
        return await _parentRepository.GetAllAsync(
            cancellationToken);
    }
}