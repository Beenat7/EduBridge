namespace EduBridge.Application.Subjects.DTOs.Responses;

public sealed record SubjectResponseDto(
    Guid Id,
    Guid SchoolId,
    string Name,
    string Code,
    string Description,
    string Status);