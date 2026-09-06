using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Subjects.Commands;

public sealed record CreateSubjectCommand(
    Guid SchoolId,
    string Name,
    string Code,
    string Description)
    : IRequest<Subject>;

public sealed class CreateSubjectCommandHandler
    : IRequestHandler<CreateSubjectCommand, Subject>
{
    private readonly ISubjectRepository _subjectRepository;

    public CreateSubjectCommandHandler(
        ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<Subject> Handle(
        CreateSubjectCommand request,
        CancellationToken cancellationToken)
    {
        var subject = new Subject(
            request.SchoolId,
            request.Name,
            request.Code,
            request.Description);

        await _subjectRepository.AddAsync(
            subject,
            cancellationToken);

        await _subjectRepository.SaveChangesAsync(
            cancellationToken);

        return subject;
    }
}