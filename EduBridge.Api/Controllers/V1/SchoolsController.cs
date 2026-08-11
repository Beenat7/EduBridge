using EduBridge.Application.Schools.Commands.CreateSchool;
using EduBridge.Application.Schools.Queries.GetSchools;
using EduBridge.Application.Schools.Queries.GetSchoolById;
using EduBridge.Application.Schools.DTOs.Responses;
using EduBridge.Application.Schools.DTOs.Requests;
using EduBridge.Application.Schools.Commands.UpdateSchool;
using EduBridge.Application.Schools.Commands.ArchiveSchool;
using EduBridge.Application.Schools.Commands.ActivateSchool;
using EduBridge.Application.Schools.Commands.DeactivateSchool;

using MediatR;
//using MapsterMapping;

using Microsoft.AspNetCore.Mvc;

namespace EduBridge.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class SchoolsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    public SchoolsController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(SchoolResponse), StatusCodes.Status201Created)]
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
    [ProducesResponseType(typeof(IReadOnlyList<SchoolResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<SchoolResponse>>> GetAll(
        CancellationToken cancellationToken)
    {
        var schools = await _sender.Send(
            new GetSchoolsQuery(),
            cancellationToken);
        // return Ok(schools); 
        return Ok(_mapper<SchollResponseDto[]>(schools));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SchoolResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SchoolResponse>> GetById(
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
    [ProducesResponseType(typeof(SchoolResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SchoolResponse>> Update(
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
    [ProducesResponseType(typeof(SchoolResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SchoolResponse>> Archive(
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
    public async Task<ActionResult<SchoolResponse>> Activate(
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
    public async Task<ActionResult<SchoolResponse>> Deactivate(
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