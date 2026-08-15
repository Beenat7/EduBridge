namespace EduBridge.Application.Students.DTOs.Requests;

public sealed record UpdateStudentRequestDto(
    string FirstName,
    string MiddleName,
    string LastName,
    DateTime DateOfBirth,
    string Gender,
    string Grade);