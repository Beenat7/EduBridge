namespace EduBridge.Api.Authentication;

public sealed record LoginResponse(
    string Token,
    DateTime ExpiresAt);