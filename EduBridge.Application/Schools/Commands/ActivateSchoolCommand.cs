using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Schools.Commands.ActivateSchool;

public sealed record ActivateSchoolCommand(Guid Id)
    : IRequest<School?>;

public sealed class ActivateSchoolCommandHandler
    : IRequestHandler<ActivateSchoolCommand, School?>
{
    private readonly ISchoolRepository _schoolRepository;

    public ActivateSchoolCommandHandler(
        ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async Task<School?> Handle(
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

        return school;
    }
}

