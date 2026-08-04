namespace EduBridge.Application.Schools.DTOs;

public sealed record UpdateSchoolRequest(
    string Name,
    string Email,
    string PhoneNumber,
    string Address);