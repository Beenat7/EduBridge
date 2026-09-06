namespace EduBridge.Application.Teachers.DTOs.Requests;

public sealed record CreateTeacherRequestDto(
    Guid SchoolId,
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber,
    string EmployeeCode,
    DateOnly HireDate);