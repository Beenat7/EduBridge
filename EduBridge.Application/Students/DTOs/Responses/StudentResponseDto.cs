namespace EduBridge.Application.Students.DTOs.Responses;

public sealed record StudentResponseDto(
    Guid Id,
    string FirstName,
    string MiddleName,
    string LastName,
    string StudentCode,
    DateTime DateOfBirth,
    string Gender,
    Guid SchoolId,
    string Grade,
    string Status);