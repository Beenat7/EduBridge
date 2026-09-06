namespace EduBridge.Application.Subjects.DTOs.Requests;

public sealed record UpdateSubjectRequestDto(
    string Name,
    string Code,
    string Description);