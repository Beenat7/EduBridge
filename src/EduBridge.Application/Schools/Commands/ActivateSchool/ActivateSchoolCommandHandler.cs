using EduBridge.Application.Interfaces;
using EduBridge.Application.Schools.DTOs;
using MediatR;

namespace EduBridge.Application.Schools.Commands.ActivateSchool;

public sealed class ActivateSchoolCommandHandler
    : IRequestHandler<ActivateSchoolCommand, SchoolDto?>
{
    private readonly ISchoolRepository _schoolRepository;

    public ActivateSchoolCommandHandler(
        ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async Task<SchoolDto?> Handle(
        ActivateSchoolCommand request,
        CancellationToken cancellationToken)
    {
        var school = await _schoolRepository.GetByIdAsync(
            request.Id,
            cancellationToken);


        if (school is null)
        {
            return null;
        }

        school.Activate();

        await _schoolRepository.SaveChangesAsync(
            cancellationToken);

        return new SchoolDto(
            school.Id,
            school.Name,
            school.Code,
            school.Email,
            school.PhoneNumber,
            school.Address,
            school.Status.ToString());
    }
}