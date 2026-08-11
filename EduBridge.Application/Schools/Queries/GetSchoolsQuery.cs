using EduBridge.Application.Interfaces;
using EduBridge.Application.Schools.DTOs.Responses;
using MediatR;

namespace EduBridge.Application.Schools.Queries.GetSchools;

public sealed record GetSchoolsQuery
    : IRequest<IReadOnlyList<SchoolResponse>>;

public sealed class GetSchoolsQueryHandler
    : IRequestHandler<GetSchoolsQuery, IReadOnlyList<SchoolResponse>>
{
    private readonly ISchoolRepository _schoolRepository;

    public GetSchoolsQueryHandler(ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async Task<IReadOnlyList<SchoolResponse>> Handle(
        GetSchoolsQuery request,
        CancellationToken cancellationToken)
    {
        var schools = await _schoolRepository.GetAllAsync(
            cancellationToken);

        return schools
            .Select(s => new SchoolResponse(
                s.Id,
                s.Name,
                s.Code,
                s.Email,
                s.PhoneNumber,
                s.Address,
                s.Status.ToString()))
            .ToList();
    }
}    