using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Classes.Commands;

public sealed record ArchiveClassCommand(
    Guid Id)
    : IRequest<Class?>;

public sealed class ArchiveClassCommandHandler
    : IRequestHandler<ArchiveClassCommand, Class?>
{
    private readonly IClassRepository _classRepository;

    public ArchiveClassCommandHandler(
        IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<Class?> Handle(
        ArchiveClassCommand request,
        CancellationToken cancellationToken)
    {
        var @class = await _classRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (@class is null)
        {
            return null;
        }

        @class.Archive();

        await _classRepository.SaveChangesAsync(
            cancellationToken);

        return @class;
    }
}