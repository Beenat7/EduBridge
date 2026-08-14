using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Schools.Commands.ArchiveSchool;

public sealed record ArchiveSchoolCommand(Guid Id)
    : IRequest<School?>;

public sealed class ArchiveSchoolCommandHandler
    : IRequestHandler<ArchiveSchoolCommand, School?>
{
    private readonly ISchoolRepository _schoolRepository;

    public ArchiveSchoolCommandHandler(
        ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async Task<School?> Handle(
        ArchiveSchoolCommand request,
        CancellationToken cancellationToken)
    {
        var school = await _schoolRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (school is null)
        {
            return null;
        }

        school.Archive();

        await _schoolRepository.SaveChangesAsync(
            cancellationToken);

        return school;
    }
}

