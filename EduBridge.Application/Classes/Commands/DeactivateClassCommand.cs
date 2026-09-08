using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Classes.Commands;

public sealed record DeactivateClassCommand(
    Guid Id)
    : IRequest<Class?>;

public sealed class DeactivateClassCommandHandler
    : IRequestHandler<DeactivateClassCommand, Class?>
{
    private readonly IClassRepository _classRepository;

    public DeactivateClassCommandHandler(
        IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<Class?> Handle(
        DeactivateClassCommand request,
        CancellationToken cancellationToken)
    {
        var @class = await _classRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (@class is null)
        {
            return null;
        }

        @class.Deactivate();

        await _classRepository.SaveChangesAsync(
            cancellationToken);

        return @class;
    }
}