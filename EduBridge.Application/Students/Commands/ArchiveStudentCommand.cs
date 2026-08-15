using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Students.Commands;

public sealed record ArchiveStudentCommand(Guid Id)
    : IRequest<Student?>;

public sealed class ArchiveStudentCommandHandler
    : IRequestHandler<ArchiveStudentCommand, Student?>
{
    private readonly IStudentRepository _studentRepository;

    public ArchiveStudentCommandHandler(
        IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Student?> Handle(
        ArchiveStudentCommand request,
        CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (student is null)
        {
            return null;
        }

        student.Archive();

        await _studentRepository.SaveChangesAsync(
            cancellationToken);

        return student;
    }
}