namespace EduBridge.Application.Students.DTOs.Requests;

public sealed record CreateStudentRequestDto(
    string FirstName,
    string MiddleName,
    string LastName,
    string StudentCode,
    DateTime DateOfBirth,
    string Gender,
    Guid SchoolId,
    string Grade);