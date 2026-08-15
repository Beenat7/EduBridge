using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Students.Commands;

public sealed record DeactivateStudentCommand(Guid Id)
    : IRequest<Student?>;

public sealed class DeactivateStudentCommandHandler
    : IRequestHandler<DeactivateStudentCommand, Student?>
{
    private readonly IStudentRepository _studentRepository;

    public DeactivateStudentCommandHandler(
        IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Student?> Handle(
        DeactivateStudentCommand request,
        CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (student is null)
        {
            return null;
        }

        student.Deactivate();

        await _studentRepository.SaveChangesAsync(
            cancellationToken);

        return student;
    }
}