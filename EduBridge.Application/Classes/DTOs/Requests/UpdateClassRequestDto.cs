using EduBridge.Domain.Common.Enums;

namespace EduBridge.Application.Classes.DTOs.Requests;

public sealed record UpdateClassRequestDto(
    string Name,
    GradeLevel GradeLevel,
    string Section);