using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace EduBridge.Application.Classes.Commands;

public sealed record AddSubjectToClassCommand(Guid ClassId, Guid SubjectId)
    : IRequest<ClassSubject?>;

public sealed class AddSubjectToClassCommandHandler
    : IRequestHandler<AddSubjectToClassCommand, ClassSubject?>
{
    private readonly IClassRepository _classRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IClassSubjectRepository _classSubjectRepository;

    public AddSubjectToClassCommandHandler(
        IClassRepository classRepository,
        ISubjectRepository subjectRepository,
        IClassSubjectRepository classSubjectRepository)
    {
        _classRepository = classRepository;
        _subjectRepository = subjectRepository;
        _classSubjectRepository = classSubjectRepository;
    }

    public async Task<ClassSubject?> Handle(
        AddSubjectToClassCommand request,
        CancellationToken cancellationToken)
    {
        var @class = await _classRepository.GetByIdAsync(
            request.ClassId,
            cancellationToken);

        if (@class is null)
        {
            return null;
        }

        var subject = await _subjectRepository.GetByIdAsync(
            request.SubjectId,
            cancellationToken);

        if (subject is null)
        {
            return null;
        }

        if (@class.SchoolId != subject.SchoolId)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure(
                    nameof(request.SubjectId),
                    "Class and subject must belong to the same school.")
            });
        }

        var existing = await _classSubjectRepository.GetByClassAndSubjectAsync(
            request.ClassId,
            request.SubjectId,
            cancellationToken);

        if (existing is not null)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure(
                    nameof(request.SubjectId),
                    "Subject is already assigned to the class.")
            });
        }

        var classSubject = new ClassSubject(
            request.ClassId,
            request.SubjectId);

        await _classSubjectRepository.AddAsync(
            classSubject,
            cancellationToken);

        await _classSubjectRepository.SaveChangesAsync(
            cancellationToken);

        return classSubject;
    }
}
