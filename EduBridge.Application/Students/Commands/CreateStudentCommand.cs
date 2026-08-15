using EduBridge.Application.Interfaces;
using EduBridge.Domain.Common.Enums;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Students.Commands;

public sealed record CreateStudentCommand(
    string FirstName,
    string MiddleName,
    string LastName,
    string StudentCode,
    DateTime DateOfBirth,
    string Gender,
    Guid SchoolId,
    string Grade)
    : IRequest<Student>;

public sealed class CreateStudentCommandHandler
    : IRequestHandler<CreateStudentCommand, Student>
{
    private readonly IStudentRepository _studentRepository;

    public CreateStudentCommandHandler(
        IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Student> Handle(
        CreateStudentCommand request,
        CancellationToken cancellationToken)
    {
        var gender = Enum.Parse<Gender>(
            request.Gender,
            ignoreCase: true);

        var student = new Student(
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.StudentCode,
            request.DateOfBirth,
            gender,
            request.SchoolId,
            request.Grade);

        await _studentRepository.AddAsync(
            student,
            cancellationToken);

        await _studentRepository.SaveChangesAsync(
            cancellationToken);

        return student;
    }
}