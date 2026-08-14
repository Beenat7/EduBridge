using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Schools.Commands.CreateSchool;

public sealed record CreateSchoolCommand(
    string Name,
    string Code,
    string Email,
    string PhoneNumber,
    string Address)
    : IRequest<School>;

public sealed class CreateSchoolCommandHandler
    : IRequestHandler<CreateSchoolCommand, School>
{
    private readonly ISchoolRepository _schoolRepository;

    public CreateSchoolCommandHandler(
        ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async Task<School> Handle(
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

        return school;
    }
}

