using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Teachers.Commands;

public sealed record CreateTeacherCommand(
    Guid SchoolId,
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber,
    string EmployeeCode,
    DateOnly HireDate)
    : IRequest<Teacher>;

public sealed class CreateTeacherCommandHandler
    : IRequestHandler<CreateTeacherCommand, Teacher>
{
    private readonly ITeacherRepository _teacherRepository;

    public CreateTeacherCommandHandler(
        ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<Teacher> Handle(
        CreateTeacherCommand request,
        CancellationToken cancellationToken)
    {
        var teacher = new Teacher(
            request.SchoolId,
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.EmployeeCode,
            request.HireDate);

        await _teacherRepository.AddAsync(
            teacher,
            cancellationToken);

        await _teacherRepository.SaveChangesAsync(
            cancellationToken);

        return teacher;
    }
}