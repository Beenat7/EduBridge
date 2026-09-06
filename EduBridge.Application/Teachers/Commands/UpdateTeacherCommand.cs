using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Teachers.Commands;

public sealed record UpdateTeacherCommand(
    Guid Id,
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber,
    string EmployeeCode,
    DateOnly HireDate)
    : IRequest<Teacher?>;

public sealed class UpdateTeacherCommandHandler
    : IRequestHandler<UpdateTeacherCommand, Teacher?>
{
    private readonly ITeacherRepository _teacherRepository;

    public UpdateTeacherCommandHandler(
        ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<Teacher?> Handle(
        UpdateTeacherCommand request,
        CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (teacher is null)
        {
            return null;
        }

        teacher.Update(
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.EmployeeCode,
            request.HireDate);

        await _teacherRepository.SaveChangesAsync(
            cancellationToken);

        return teacher;
    }
}