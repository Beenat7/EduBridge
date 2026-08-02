namespace EduBridge.Application.Schools.DTOs;

public sealed record SchoolDto(
    Guid Id,
    string Name,
    string Code,
    string Email,
    string PhoneNumber,
    string Address,
    string Status);