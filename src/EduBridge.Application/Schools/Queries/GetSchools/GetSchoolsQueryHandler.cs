using EduBridge.Application.Interfaces;
using EduBridge.Application.Schools.DTOs;
using MediatR;

namespace EduBridge.Application.Schools.Queries.GetSchools;

public sealed class GetSchoolsQueryHandler
    : IRequestHandler<GetSchoolsQuery, IReadOnlyList<SchoolDto>>
{
    private readonly ISchoolRepository _schoolRepository;

    public GetSchoolsQueryHandler(ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async Task<IReadOnlyList<SchoolDto>> Handle(
        GetSchoolsQuery request,
        CancellationToken cancellationToken)
    {
        var schools = await _schoolRepository.GetAllAsync(
            cancellationToken);

        return schools
            .Select(s => new SchoolDto(
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