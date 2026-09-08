namespace EduBridge.Application.Teachers.DTOs.Responses;

public sealed record TeacherSubjectResponseDto(
    Guid Id,
    string Name,
    string Code,
    string Status);
