namespace EduBridge.Api.Authentication;

public sealed record CurrentUserResponse(
    Guid Id,
    string Email,
    IList<string> Roles);