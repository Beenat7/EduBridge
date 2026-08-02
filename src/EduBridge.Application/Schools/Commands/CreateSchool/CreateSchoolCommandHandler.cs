using EduBridge.Application.Interfaces;
using EduBridge.Application.Schools.DTOs;
using EduBridge.Domain.Schools;
using MediatR;

namespace EduBridge.Application.Schools.Commands.CreateSchool;

public sealed class CreateSchoolCommandHandler 
    : IRequestHandler<CreateSchoolCommand, SchoolDto>
{
    private readonly ISchoolRepository _schoolRepository;

    public CreateSchoolCommandHandler(
        ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async Task<SchoolDto> Handle(
        CreateSchoolCommand request,
        CancellationToken cancellationToken)
    {
        var school = new School(
            request.Name,
            request.Code,
            request.Email,
            request.PhoneNumber,
            request.Address);

        await _schoolRepository.AddAsync(
            school,
            cancellationToken);

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