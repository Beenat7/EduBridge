namespace EduBridge.Application.Teachers.DTOs.Responses;

public sealed record TeacherResponseDto(
    Guid Id,
    Guid SchoolId,
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber,
    string EmployeeCode,
    DateOnly HireDate,
    string Status);