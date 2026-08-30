namespace EduBridge.Application.Interfaces;

public interface IJwtTokenService
{
    Task<string> CreateTokenAsync(Guid userId);
}