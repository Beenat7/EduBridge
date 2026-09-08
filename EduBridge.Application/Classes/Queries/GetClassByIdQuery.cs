using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Classes.Queries;

public sealed record GetClassByIdQuery(
    Guid Id)
    : IRequest<Class?>;

public sealed class GetClassByIdQueryHandler
    : IRequestHandler<GetClassByIdQuery, Class?>
{
    private readonly IClassRepository _classRepository;

    public GetClassByIdQueryHandler(
        IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<Class?> Handle(
        GetClassByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _classRepository.GetByIdAsync(
            request.Id,
            cancellationToken);
    }
}