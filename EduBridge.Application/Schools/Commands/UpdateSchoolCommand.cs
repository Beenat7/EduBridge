using EduBridge.Application.Interfaces;
using EduBridge.Application.Schools.DTOs.Responses;
using MediatR;
//update school validator like create 
namespace EduBridge.Application.Schools.Commands.UpdateSchool;

public sealed record UpdateSchoolCommand(
    Guid Id,
    string Name,
    string Email,
    string PhoneNumber,
    string Address)
    : IRequest<SchoolResponse?>;


public sealed class UpdateSchoolCommandHandler
    : IRequestHandler<UpdateSchoolCommand, SchoolResponse?>
{
    private readonly ISchoolRepository _schoolRepository;

    public UpdateSchoolCommandHandler(
        ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }


    public async Task<SchoolResponse?> Handle(
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