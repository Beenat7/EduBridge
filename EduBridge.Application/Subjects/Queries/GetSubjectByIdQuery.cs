using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Subjects.Queries;

public sealed record GetSubjectByIdQuery(
    Guid Id)
    : IRequest<Subject?>;

public sealed class GetSubjectByIdQueryHandler
    : IRequestHandler<GetSubjectByIdQuery, Subject?>
{
    private readonly ISubjectRepository _subjectRepository;

    public GetSubjectByIdQueryHandler(
        ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<Subject?> Handle(
        GetSubjectByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _subjectRepository.GetByIdAsync(
            request.Id,
            cancellationToken);
    }
}