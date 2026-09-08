namespace EduBridge.Application.Subjects.DTOs.Responses;

public sealed record SubjectTeacherResponseDto(
    Guid Id,
    string FirstName,
    string MiddleName,
    string LastName,
    string EmployeeCode,
    string Status);
