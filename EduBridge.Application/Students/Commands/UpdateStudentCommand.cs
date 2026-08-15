using EduBridge.Application.Interfaces;
using EduBridge.Domain.Common.Enums;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Students.Commands;

public sealed record UpdateStudentCommand(
    Guid Id,
    string FirstName,
    string MiddleName,
    string LastName,
    DateTime DateOfBirth,
    string Gender,
    string Grade)
    : IRequest<Student?>;

public sealed class UpdateStudentCommandHandler
    : IRequestHandler<UpdateStudentCommand, Student?>
{
    private readonly IStudentRepository _studentRepository;

    public UpdateStudentCommandHandler(
        IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Student?> Handle(
        UpdateStudentCommand request,
        CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (student is null)
        {
            return null;
        }

        var gender = Enum.Parse<Gender>(
            request.Gender,
            ignoreCase: true);

        student.UpdatePersonalInformation(
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.DateOfBirth,
            gender);

        student.UpdateGrade(request.Grade);

        await _studentRepository.SaveChangesAsync(
            cancellationToken);

        return student;
    }
}