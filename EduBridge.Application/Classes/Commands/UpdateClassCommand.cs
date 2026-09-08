using EduBridge.Application.Interfaces;
using EduBridge.Domain.Common.Enums;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Classes.Commands;

public sealed record UpdateClassCommand(
    Guid Id,
    string Name,
    GradeLevel GradeLevel,
    string Section)
    : IRequest<Class?>;

public sealed class UpdateClassCommandHandler
    : IRequestHandler<UpdateClassCommand, Class?>
{
    private readonly IClassRepository _classRepository;

    public UpdateClassCommandHandler(
        IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<Class?> Handle(
        UpdateClassCommand request,
        CancellationToken cancellationToken)
    {
        var @class = await _classRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (@class is null)
        {
            return null;
        }

        @class.Update(
            request.Name,
            request.GradeLevel,
            request.Section);

        await _classRepository.SaveChangesAsync(
            cancellationToken);

        return @class;
    }
}