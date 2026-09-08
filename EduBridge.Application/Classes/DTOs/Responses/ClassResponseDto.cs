namespace EduBridge.Application.Classes.DTOs.Responses;

public sealed record ClassResponseDto(
    Guid Id,
    Guid SchoolId,
    string Name,
    string GradeLevel,
    string Section,
    string Status);