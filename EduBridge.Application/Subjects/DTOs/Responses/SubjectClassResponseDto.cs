namespace EduBridge.Application.Subjects.DTOs.Responses;

public sealed record SubjectClassResponseDto(
    Guid Id,
    string Name,
    string GradeLevel,
    string Section,
    string Status);
