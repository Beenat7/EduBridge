namespace EduBridge.Application.Parents.DTOs.Responses;

public sealed record ParentResponseDto(
    Guid Id,
    Guid SchoolId,
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Status);