namespace EduBridge.Api.Contracts.Auth;

public sealed record LoginResponse(
    string Token,
    DateTime ExpiresAt);