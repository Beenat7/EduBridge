using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Classes.Commands;

public sealed record ActivateClassCommand(
    Guid Id)
    : IRequest<Class?>;

public sealed class ActivateClassCommandHandler
    : IRequestHandler<ActivateClassCommand, Class?>
{
    private readonly IClassRepository _classRepository;

    public ActivateClassCommandHandler(
        IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<Class?> Handle(
        ActivateClassCommand request,
        CancellationToken cancellationToken)
    {
        var @class = await _classRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (@class is null)
        {
            return null;
        }

        @class.Activate();

        await _classRepository.SaveChangesAsync(
            cancellationToken);

        return @class;
    }
}