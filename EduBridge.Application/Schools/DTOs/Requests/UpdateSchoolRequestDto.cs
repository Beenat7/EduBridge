namespace EduBridge.Application.Schools.DTOs.Requests;

public sealed record UpdateSchoolRequestDto(
    string Name,
    string Email,
    string PhoneNumber,
    string Address);