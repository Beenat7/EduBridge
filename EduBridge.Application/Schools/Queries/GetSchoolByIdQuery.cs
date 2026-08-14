using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Schools.Queries.GetSchoolById;

public sealed record GetSchoolByIdQuery(Guid Id)
    : IRequest<School?>;

public sealed class GetSchoolByIdQueryHandler
    : IRequestHandler<GetSchoolByIdQuery, School?>
{
    private readonly ISchoolRepository _schoolRepository;

    public GetSchoolByIdQueryHandler(
        ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async Task<School?> Handle(
        GetSchoolByIdQuery request,
        CancellationToken cancellationToken)
    {
        var school = await _schoolRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        return school;
    }
}
