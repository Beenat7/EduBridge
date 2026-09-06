namespace EduBridge.Application.Parents.DTOs.Requests;

public sealed record CreateParentRequestDto(
    Guid SchoolId,
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber);
    