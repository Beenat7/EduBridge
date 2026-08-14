using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Schools.Commands.DeactivateSchool;

public sealed record DeactivateSchoolCommand(Guid Id)
    : IRequest<School?>;

public sealed class DeactivateSchoolCommandHandler
    : IRequestHandler<DeactivateSchoolCommand, School?>
{
    private readonly ISchoolRepository _schoolRepository;

    public DeactivateSchoolCommandHandler(
        ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async Task<School?> Handle(
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

        return school;
    }
}
