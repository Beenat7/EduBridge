using EduBridge.Application.Interfaces;
using EduBridge.Application.Schools.DTOs.Responses;
using MediatR;

namespace EduBridge.Application.Schools.Queries.GetSchoolById;

public sealed record GetSchoolByIdQuery(Guid Id)
    : IRequest<SchoolResponse?>;


public sealed class GetSchoolByIdQueryHandler
    : IRequestHandler<GetSchoolByIdQuery, SchoolResponse?>
{
    private readonly ISchoolRepository _schoolRepository;

    public GetSchoolByIdQueryHandler(
        ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async Task<SchoolResponse?> Handle(
        GetSchoolByIdQuery request,
        CancellationToken cancellationToken)
    {
        var school = await _schoolRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (school is null)
        {
            return null;
        }

        return new SchoolResponse(
            school.Id,
            school.Name,
            school.Code,
            school.Email,
            school.PhoneNumber,
            school.Address,
            school.Status.ToString());
    }
}    