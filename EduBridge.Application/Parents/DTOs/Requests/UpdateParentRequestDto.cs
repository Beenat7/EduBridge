namespace EduBridge.Application.Parents.DTOs.Requests;

public sealed record UpdateParentRequestDto(
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber);