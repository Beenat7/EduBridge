using EduBridge.Domain.Common.Enums;

namespace EduBridge.Application.Classes.DTOs.Requests;

public sealed record CreateClassRequestDto(
    Guid SchoolId,
    string Name,
    GradeLevel GradeLevel,
    string Section);