using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Classes.Queries;

public sealed record GetClassesQuery
    : IRequest<IReadOnlyList<Class>>;

public sealed class GetClassesQueryHandler
    : IRequestHandler<GetClassesQuery, IReadOnlyList<Class>>
{
    private readonly IClassRepository _classRepository;

    public GetClassesQueryHandler(
        IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<IReadOnlyList<Class>> Handle(
        GetClassesQuery request,
        CancellationToken cancellationToken)
    {
        return await _classRepository.GetAllAsync(
            cancellationToken);
    }
}