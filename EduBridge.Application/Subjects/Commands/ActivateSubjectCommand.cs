using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Subjects.Commands;

public sealed record ActivateSubjectCommand(
    Guid Id)
    : IRequest<Subject?>;

public sealed class ActivateSubjectCommandHandler
    : IRequestHandler<ActivateSubjectCommand, Subject?>
{
    private readonly ISubjectRepository _subjectRepository;

    public ActivateSubjectCommandHandler(
        ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<Subject?> Handle(
        ActivateSubjectCommand request,
        CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (subject is null)
        {
            return null;
        }

        subject.Activate();

        await _subjectRepository.SaveChangesAsync(
            cancellationToken);

        return subject;
    }
}