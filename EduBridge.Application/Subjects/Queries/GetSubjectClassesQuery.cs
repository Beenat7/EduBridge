using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Subjects.Queries;

public sealed record GetSubjectClassesQuery(Guid SubjectId)
    : IRequest<IReadOnlyList<Class>?>;

public sealed class GetSubjectClassesQueryHandler
    : IRequestHandler<GetSubjectClassesQuery, IReadOnlyList<Class>?>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IClassSubjectRepository _classSubjectRepository;

    public GetSubjectClassesQueryHandler(
        ISubjectRepository subjectRepository,
        IClassSubjectRepository classSubjectRepository)
    {
        _subjectRepository = subjectRepository;
        _classSubjectRepository = classSubjectRepository;
    }

    public async Task<IReadOnlyList<Class>?> Handle(
        GetSubjectClassesQuery request,
        CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.GetByIdAsync(
            request.SubjectId,
            cancellationToken);

        if (subject is null)
        {
            return null;
        }

        return await _classSubjectRepository.GetClassesBySubjectIdAsync(
            request.SubjectId,
            cancellationToken);
    }
}
