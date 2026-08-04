using EduBridge.Application.Schools.Commands.CreateSchool;
using EduBridge.Application.Schools.Queries.GetSchools;
using EduBridge.Application.Schools.Queries.GetSchoolById;
using EduBridge.Application.Schools.DTOs;
using EduBridge.Application.Schools.Commands.UpdateSchool;
using EduBridge.Application.Schools.Commands.ArchiveSchool;
using EduBridge.Application.Schools.Commands.ActivateSchool;
using EduBridge.Application.Schools.Commands.DeactivateSchool;

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
    [ProducesResponseType(typeof(SchoolDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        CreateSchoolCommand command,
        CancellationToken cancellationToken)
    {
        var school = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(
        nameof(GetById),
        new { id = school.Id },
        school);

    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<SchoolDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<SchoolDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var schools = await _sender.Send(
            new GetSchoolsQuery(),
            cancellationToken);

        return Ok(schools);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SchoolDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SchoolDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var school = await _sender.Send(
            new GetSchoolByIdQuery(id),
            cancellationToken);

        if (school is null)
        {
            return NotFound();
        }

        return Ok(school);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(SchoolDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SchoolDto>> Update(
        Guid id,
        UpdateSchoolRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateSchoolCommand(
            id,
            request.Name,
            request.Email,
            request.PhoneNumber,
            request.Address);

        var school = await _sender.Send(
            command,
            cancellationToken);

        if (school is null)
        {
            return NotFound();
        }

        return Ok(school);
    }

    [HttpPost("{id:guid}/archive")]
    [ProducesResponseType(typeof(SchoolDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SchoolDto>> Archive(
        Guid id,
        CancellationToken cancellationToken)
    {
        var school = await _sender.Send(
            new ArchiveSchoolCommand(id),
            cancellationToken);

        if (school is null)
        {
            return NotFound();
        }

        return Ok(school);
    }
    

    [HttpPut("{id:guid}/activate")]
    public async Task<ActionResult<SchoolDto>> Activate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var school = await _sender.Send(
            new ActivateSchoolCommand(id),
            cancellationToken);

        if (school is null)
        {
            return NotFound();
        }

        return Ok(school);
    }
 

    [HttpPut("{id:guid}/deactivate")]
    public async Task<ActionResult<SchoolDto>> Deactivate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var school = await _sender.Send(
            new DeactivateSchoolCommand(id),
            cancellationToken);

        if (school is null)
        {
            return NotFound();
        }

        return Ok(school);
    }

}