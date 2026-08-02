using EduBridge.Application.Schools.Commands.CreateSchool;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduBridge.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class SchoolsController : ControllerBase
{
    private readonly ISender _sender;
    public SchoolsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateSchoolCommand command,
        CancellationToken cancellationToken)
    {
        var school = await _sender.Send(command, cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created, school);
    }
}