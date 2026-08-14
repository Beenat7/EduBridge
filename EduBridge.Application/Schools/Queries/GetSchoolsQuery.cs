using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Schools.Queries.GetSchools;

public sealed record GetSchoolsQuery
    : IRequest<IReadOnlyList<School>>;

public sealed class GetSchoolsQueryHandler
    : IRequestHandler<GetSchoolsQuery, IReadOnlyList<School>>
{
    private readonly ISchoolRepository _schoolRepository;

    public GetSchoolsQueryHandler(
        ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async Task<IReadOnlyList<School>> Handle(
        GetSchoolsQuery request,
        CancellationToken cancellationToken)
    {
        return await _schoolRepository.GetAllAsync(
            cancellationToken);
    }
}
