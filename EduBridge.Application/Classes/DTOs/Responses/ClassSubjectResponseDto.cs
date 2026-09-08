namespace EduBridge.Application.Classes.DTOs.Responses;

public sealed record ClassSubjectResponseDto(
    Guid Id,
    string Name,
    string Code,
    string Status);
