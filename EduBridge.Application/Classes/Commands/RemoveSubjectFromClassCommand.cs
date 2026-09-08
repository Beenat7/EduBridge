using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Classes.Commands;

public sealed record RemoveSubjectFromClassCommand(Guid ClassId, Guid SubjectId)
    : IRequest<ClassSubject?>;

public sealed class RemoveSubjectFromClassCommandHandler
    : IRequestHandler<RemoveSubjectFromClassCommand, ClassSubject?>
{
    private readonly IClassSubjectRepository _classSubjectRepository;

    public RemoveSubjectFromClassCommandHandler(
        IClassSubjectRepository classSubjectRepository)
    {
        _classSubjectRepository = classSubjectRepository;
    }

    public async Task<ClassSubject?> Handle(
        RemoveSubjectFromClassCommand request,
        CancellationToken cancellationToken)
    {
        var classSubject = await _classSubjectRepository
            .GetByClassAndSubjectAsync(
                request.ClassId,
                request.SubjectId,
                cancellationToken);

        if (classSubject is null)
        {
            return null;
        }

        _classSubjectRepository.Remove(classSubject);

        await _classSubjectRepository.SaveChangesAsync(
            cancellationToken);

        return classSubject;
    }
}
