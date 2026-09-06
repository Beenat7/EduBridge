using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Subjects.Commands;

public sealed record DeactivateSubjectCommand(
    Guid Id)
    : IRequest<Subject?>;

public sealed class DeactivateSubjectCommandHandler
    : IRequestHandler<DeactivateSubjectCommand, Subject?>
{
    private readonly ISubjectRepository _subjectRepository;

    public DeactivateSubjectCommandHandler(
        ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<Subject?> Handle(
        DeactivateSubjectCommand request,
        CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (subject is null)
        {
            return null;
        }

        subject.Deactivate();

        await _subjectRepository.SaveChangesAsync(
            cancellationToken);

        return subject;
    }
}