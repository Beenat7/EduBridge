namespace EduBridge.Application.Schools.DTOs.Responses;

public sealed record SchoolResponseDto(
    Guid Id,
    string Name,
    string Code,
    string Email,
    string PhoneNumber,
    string Address,
    string Status);