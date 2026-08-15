using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Students.Commands;

public sealed record ActivateStudentCommand(Guid Id)
    : IRequest<Student?>;

public sealed class ActivateStudentCommandHandler
    : IRequestHandler<ActivateStudentCommand, Student?>
{
    private readonly IStudentRepository _studentRepository;

    public ActivateStudentCommandHandler(
        IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Student?> Handle(
        ActivateStudentCommand request,
        CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (student is null)
        {
            return null;
        }

        student.Activate();

        await _studentRepository.SaveChangesAsync(
            cancellationToken);

        return student;
    }
}