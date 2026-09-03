using EduBridge.Api.Authentication;
using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace EduBridge.Api.Controllers.V1;



[ApiController]
[Route("api/v1/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly UserManager<EduBridgeUser> _userManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserManager<EduBridgeUser> userManager,
        IJwtTokenService jwtTokenService,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
        _configuration = configuration;
    }

    [HttpPost("login")]
    [ProducesResponseType(
        typeof(LoginResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResponse>> Login(
        LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Unauthorized();
        }

        var passwordValid =
            await _userManager.CheckPasswordAsync(
                user,
                request.Password);

        if (!passwordValid)
        {
            return Unauthorized();
        }

        var token = await _jwtTokenService.CreateTokenAsync(user.Id);

        var expirationMinutes =
            _configuration.GetValue<int>("Jwt:ExpirationMinutes");

        var expiresAt = DateTime.UtcNow
            .AddMinutes(expirationMinutes);

        return Ok(new LoginResponse(token, expiresAt));
    }

            [HttpGet("me")]
        [Authorize]
        [ProducesResponseType(
            typeof(CurrentUserResponse),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CurrentUserResponse>> Me()
        {
            var userIdValue = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdValue, out var userId))
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(
                userId.ToString());

            if (user is null)
            {
                return Unauthorized();
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new CurrentUserResponse(
                user.Id,
                user.Email ?? string.Empty,
                roles));
        }

}