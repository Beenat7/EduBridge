using EduBridge.Application.Schools.Commands.ActivateSchool;
using EduBridge.Application.Schools.Commands.ArchiveSchool;
using EduBridge.Application.Schools.Commands.CreateSchool;
using EduBridge.Application.Schools.Commands.DeactivateSchool;
using EduBridge.Application.Schools.Commands.UpdateSchool;
using EduBridge.Application.Schools.DTOs.Requests;
using EduBridge.Application.Schools.DTOs.Responses;
using EduBridge.Application.Schools.Queries.GetSchoolById;
using EduBridge.Application.Schools.Queries.GetSchools;

using MapsterMapper;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace EduBridge.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class SchoolsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public SchoolsController(
        ISender sender,
        IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(
        typeof(SchoolResponseDto),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SchoolResponseDto>> Create(
        CreateSchoolCommand command,
        CancellationToken cancellationToken)
    {
        var school = await _sender.Send(
            command,
            cancellationToken);

        var response = _mapper.Map<SchoolResponseDto>(school);

        return CreatedAtAction(
            nameof(GetById),
            new { id = school.Id },
            response);
    }

    [HttpGet]
    [ProducesResponseType(
        typeof(IReadOnlyList<SchoolResponseDto>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<SchoolResponseDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var schools = await _sender.Send(
            new GetSchoolsQuery(),
            cancellationToken);

        var response = _mapper.Map<IReadOnlyList<SchoolResponseDto>>(
            schools);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(
        typeof(SchoolResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SchoolResponseDto>> GetById(
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

        var response = _mapper.Map<SchoolResponseDto>(school);

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(
        typeof(SchoolResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SchoolResponseDto>> Update(
        Guid id,
        UpdateSchoolRequestDto request,
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

        var response = _mapper.Map<SchoolResponseDto>(school);

        return Ok(response);
    }

    [HttpPost("{id:guid}/archive")]
    [ProducesResponseType(
        typeof(SchoolResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SchoolResponseDto>> Archive(
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

        var response = _mapper.Map<SchoolResponseDto>(school);

        return Ok(response);
    }

    [HttpPut("{id:guid}/activate")]
    [ProducesResponseType(
        typeof(SchoolResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SchoolResponseDto>> Activate(
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

        var response = _mapper.Map<SchoolResponseDto>(school);

        return Ok(response);
    }

    [HttpPut("{id:guid}/deactivate")]
    [ProducesResponseType(
        typeof(SchoolResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SchoolResponseDto>> Deactivate(
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

        var response = _mapper.Map<SchoolResponseDto>(school);

        return Ok(response);
    }
}
