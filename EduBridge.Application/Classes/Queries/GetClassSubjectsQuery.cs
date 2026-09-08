using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Classes.Queries;

public sealed record GetClassSubjectsQuery(Guid ClassId)
    : IRequest<IReadOnlyList<Subject>?>;

public sealed class GetClassSubjectsQueryHandler
    : IRequestHandler<GetClassSubjectsQuery, IReadOnlyList<Subject>?>
{
    private readonly IClassRepository _classRepository;
    private readonly IClassSubjectRepository _classSubjectRepository;

    public GetClassSubjectsQueryHandler(
        IClassRepository classRepository,
        IClassSubjectRepository classSubjectRepository)
    {
        _classRepository = classRepository;
        _classSubjectRepository = classSubjectRepository;
    }

    public async Task<IReadOnlyList<Subject>?> Handle(
        GetClassSubjectsQuery request,
        CancellationToken cancellationToken)
    {
        var @class = await _classRepository.GetByIdAsync(
            request.ClassId,
            cancellationToken);

        if (@class is null)
        {
            return null;
        }

        return await _classSubjectRepository.GetSubjectsByClassIdAsync(
            request.ClassId,
            cancellationToken);
    }
}
