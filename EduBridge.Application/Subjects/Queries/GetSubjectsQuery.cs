using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Subjects.Queries;

public sealed record GetSubjectsQuery
    : IRequest<IReadOnlyList<Subject>>;

public sealed class GetSubjectsQueryHandler
    : IRequestHandler<GetSubjectsQuery, IReadOnlyList<Subject>>
{
    private readonly ISubjectRepository _subjectRepository;

    public GetSubjectsQueryHandler(
        ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<IReadOnlyList<Subject>> Handle(
        GetSubjectsQuery request,
        CancellationToken cancellationToken)
    {
        return await _subjectRepository.GetAllAsync(
            cancellationToken);
    }
}