using EduBridge.Application.Interfaces;
using EduBridge.Domain.Common.Enums;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Classes.Commands;

public sealed record CreateClassCommand(
    Guid SchoolId,
    string Name,
    GradeLevel GradeLevel,
    string Section)
    : IRequest<Class>;

public sealed class CreateClassCommandHandler
    : IRequestHandler<CreateClassCommand, Class>
{
    private readonly IClassRepository _classRepository;

    public CreateClassCommandHandler(
        IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<Class> Handle(
        CreateClassCommand request,
        CancellationToken cancellationToken)
    {
        var @class = new Class(
            request.SchoolId,
            request.Name,
            request.GradeLevel,
            request.Section);

        await _classRepository.AddAsync(
            @class,
            cancellationToken);

        await _classRepository.SaveChangesAsync(
            cancellationToken);

        return @class;
    }
}