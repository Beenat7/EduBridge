using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Subjects.Commands;

public sealed record ArchiveSubjectCommand(
    Guid Id)
    : IRequest<Subject?>;

public sealed class ArchiveSubjectCommandHandler
    : IRequestHandler<ArchiveSubjectCommand, Subject?>
{
    private readonly ISubjectRepository _subjectRepository;

    public ArchiveSubjectCommandHandler(
        ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<Subject?> Handle(
        ArchiveSubjectCommand request,
        CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (subject is null)
        {
            return null;
        }

        subject.Archive();

        await _subjectRepository.SaveChangesAsync(
            cancellationToken);

        return subject;
    }
}