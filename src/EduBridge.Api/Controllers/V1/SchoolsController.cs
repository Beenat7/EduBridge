using EduBridge.Application.Schools.Commands.CreateSchool;
using EduBridge.Application.Schools.Queries.GetSchools;
using EduBridge.Application.Schools.DTOs;

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

    [HttpGet]
public async Task<ActionResult<IReadOnlyList<SchoolDto>>> GetAll(
    CancellationToken cancellationToken)
{
    var schools = await _sender.Send(
        new GetSchoolsQuery(),
        cancellationToken);

    return Ok(schools);
}



}