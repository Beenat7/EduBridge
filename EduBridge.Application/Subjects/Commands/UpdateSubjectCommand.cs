using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Subjects.Commands;

public sealed record UpdateSubjectCommand(
    Guid Id,
    string Name,
    string Code,
    string Description)
    : IRequest<Subject?>;

public sealed class UpdateSubjectCommandHandler
    : IRequestHandler<UpdateSubjectCommand, Subject?>
{
    private readonly ISubjectRepository _subjectRepository;

    public UpdateSubjectCommandHandler(
        ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<Subject?> Handle(
        UpdateSubjectCommand request,
        CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (subject is null)
        {
            return null;
        }

        subject.Update(
            request.Name,
            request.Code,
            request.Description);

        await _subjectRepository.SaveChangesAsync(
            cancellationToken);

        return subject;
    }
}