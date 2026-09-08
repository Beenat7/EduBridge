using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Students.Commands;

public sealed record RemoveStudentFromClassCommand(Guid StudentId)
    : IRequest<Student?>;

public sealed class RemoveStudentFromClassCommandHandler
    : IRequestHandler<RemoveStudentFromClassCommand, Student?>
{
    private readonly IStudentRepository _studentRepository;

    public RemoveStudentFromClassCommandHandler(
        IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Student?> Handle(
        RemoveStudentFromClassCommand request,
        CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(
            request.StudentId,
            cancellationToken);

        if (student is null)
        {
            return null;
        }

        student.RemoveFromClass();

        await _studentRepository.SaveChangesAsync(
            cancellationToken);

        return student;
    }
}
