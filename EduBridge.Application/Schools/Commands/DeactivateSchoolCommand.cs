using EduBridge.Application.Interfaces;
using EduBridge.Application.Schools.DTOs.Responses;
using MediatR;

namespace EduBridge.Application.Schools.Commands.DeactivateSchool;

public sealed record DeactivateSchoolCommand(
    Guid Id
) : IRequest<SchoolResponse?>;

public sealed class DeactivateSchoolCommandHandler
    : IRequestHandler<DeactivateSchoolCommand, SchoolResponse?>
{
    private readonly ISchoolRepository _schoolRepository;

    public DeactivateSchoolCommandHandler(
        ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async Task<SchoolResponse?> Handle(
        DeactivateSchoolCommand request,
        CancellationToken cancellationToken)
    {
        var school = await _schoolRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (school is null)
        {
            return null;
        }

        school.Deactivate();

        await _schoolRepository.SaveChangesAsync(
            cancellationToken);

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