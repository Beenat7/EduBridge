namespace EduBridge.Application.Subjects.DTOs.Requests;

public sealed record CreateSubjectRequestDto(
    Guid SchoolId,
    string Name,
    string Code,
    string Description);