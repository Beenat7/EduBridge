using EduBridge.Application.Interfaces;
using EduBridge.Application.Schools.DTOs.Responses;
using EduBridge.Domain.Entities;
using MediatR;
//remove handler files
namespace EduBridge.Application.Schools.Commands.CreateSchool;

public sealed record CreateSchoolCommand(
    string Name,
    string Code,
    string Email,
    string PhoneNumber,
    string Address)
    : IRequest<SchoolResponse>;

public sealed class CreateSchoolCommandHandler 
    : IRequestHandler<CreateSchoolCommand, SchoolResponse>
{
    private readonly ISchoolRepository _schoolRepository;

    public CreateSchoolCommandHandler(
        ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async Task<SchoolResponse> Handle(
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