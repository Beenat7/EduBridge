using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace EduBridge.Application.Teachers.Commands;

public sealed record AssignSubjectToTeacherCommand(Guid TeacherId, Guid SubjectId)
    : IRequest<TeacherSubject?>;

public sealed class AssignSubjectToTeacherCommandHandler
    : IRequestHandler<AssignSubjectToTeacherCommand, TeacherSubject?>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly ITeacherSubjectRepository _teacherSubjectRepository;

    public AssignSubjectToTeacherCommandHandler(
        ITeacherRepository teacherRepository,
        ISubjectRepository subjectRepository,
        ITeacherSubjectRepository teacherSubjectRepository)
    {
        _teacherRepository = teacherRepository;
        _subjectRepository = subjectRepository;
        _teacherSubjectRepository = teacherSubjectRepository;
    }

    public async Task<TeacherSubject?> Handle(
        AssignSubjectToTeacherCommand request,
        CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetByIdAsync(
            request.TeacherId,
            cancellationToken);

        if (teacher is null)
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

        if (teacher.SchoolId != subject.SchoolId)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure(
                    nameof(request.SubjectId),
                    "Teacher and subject must belong to the same school.")
            });
        }

        var existing = await _teacherSubjectRepository
            .GetByTeacherAndSubjectAsync(
                request.TeacherId,
                request.SubjectId,
                cancellationToken);

        if (existing is not null)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure(
                    nameof(request.SubjectId),
                    "Subject is already assigned to the teacher.")
            });
        }

        var teacherSubject = new TeacherSubject(
            request.TeacherId,
            request.SubjectId);

        await _teacherSubjectRepository.AddAsync(
            teacherSubject,
            cancellationToken);

        await _teacherSubjectRepository.SaveChangesAsync(
            cancellationToken);

        return teacherSubject;
    }
}
