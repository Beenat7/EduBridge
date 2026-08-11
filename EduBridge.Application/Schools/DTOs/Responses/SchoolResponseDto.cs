namespace EduBridge.Application.Schools.DTOs.Responses;

public sealed record SchoolResponse(
    Guid Id,
    string Name,
    string Code,
    string Email,
    string PhoneNumber,
    string Address,
    string Status);