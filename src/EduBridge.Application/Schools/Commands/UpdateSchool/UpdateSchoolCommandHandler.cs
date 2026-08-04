using EduBridge.Application.Interfaces;
using EduBridge.Application.Schools.DTOs;
using MediatR;

namespace EduBridge.Application.Schools.Commands.UpdateSchool;

public sealed class UpdateSchoolCommandHandler
    : IRequestHandler<UpdateSchoolCommand, SchoolDto?>
{
    private readonly ISchoolRepository _schoolRepository;

    public UpdateSchoolCommandHandler(
        ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }


    public async Task<SchoolDto?> Handle(
        UpdateSchoolCommand request,
        CancellationToken cancellationToken)
    {
        var school = await _schoolRepository.GetByIdAsync(
            request.Id,
            cancellationToken);


        if (school is null)
        {
            return null;
        }


        school.Rename(request.Name);

        school.UpdateContactInformation(
            request.Email,
            request.PhoneNumber,
            request.Address);


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