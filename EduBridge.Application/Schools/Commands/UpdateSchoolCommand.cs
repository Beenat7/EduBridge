using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Schools.Commands.UpdateSchool;

public sealed record UpdateSchoolCommand(
    Guid Id,
    string Name,
    string Email,
    string PhoneNumber,
    string Address)
    : IRequest<School?>;

public sealed class UpdateSchoolCommandHandler
    : IRequestHandler<UpdateSchoolCommand, School?>
{
    private readonly ISchoolRepository _schoolRepository;

    public UpdateSchoolCommandHandler(
        ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async Task<School?> Handle(
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

        return school;
    }
}
