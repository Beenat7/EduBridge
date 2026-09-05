using EduBridge.Application.Parents.Commands;
using EduBridge.Application.Parents.DTOs.Requests;
using EduBridge.Application.Parents.DTOs.Responses;
using EduBridge.Application.Parents.Queries;

using MapsterMapper;
using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduBridge.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public sealed class ParentsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ParentsController(
        ISender sender,
        IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(Roles = "PlatformAdmin,SchoolAdmin")]
    [ProducesResponseType(
        typeof(ParentResponseDto),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ParentResponseDto>> Create(
        CreateParentCommand command,
        CancellationToken cancellationToken)
    {
        var parent = await _sender.Send(
            command,
            cancellationToken);

        var response = _mapper.Map<ParentResponseDto>(parent);

        return CreatedAtAction(
            nameof(GetById),
            new { id = parent.Id },
            response);
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(
        typeof(IReadOnlyList<ParentResponseDto>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ParentResponseDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var parents = await _sender.Send(
            new GetParentsQuery(),
            cancellationToken);

        var response =
            _mapper.Map<IReadOnlyList<ParentResponseDto>>(parents);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    [ProducesResponseType(
        typeof(ParentResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ParentResponseDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var parent = await _sender.Send(
            new GetParentByIdQuery(id),
            cancellationToken);

        if (parent is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<ParentResponseDto>(parent);

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "PlatformAdmin,SchoolAdmin")]
    [ProducesResponseType(
        typeof(ParentResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ParentResponseDto>> Update(
        Guid id,
        UpdateParentRequestDto request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateParentCommand(
            id,
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.Email,
            request.PhoneNumber);

        var parent = await _sender.Send(
            command,
            cancellationToken);

        if (parent is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<ParentResponseDto>(parent);

        return Ok(response);
    }

    [HttpPut("{id:guid}/activate")]
    [Authorize(Roles = "PlatformAdmin,SchoolAdmin")]
    [ProducesResponseType(
        typeof(ParentResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ParentResponseDto>> Activate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var parent = await _sender.Send(
            new ActivateParentCommand(id),
            cancellationToken);

        if (parent is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<ParentResponseDto>(parent);

        return Ok(response);
    }

    [HttpPut("{id:guid}/deactivate")]
    [Authorize(Roles = "PlatformAdmin,SchoolAdmin")]
    [ProducesResponseType(
        typeof(ParentResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ParentResponseDto>> Deactivate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var parent = await _sender.Send(
            new DeactivateParentCommand(id),
            cancellationToken);

        if (parent is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<ParentResponseDto>(parent);

        return Ok(response);
    }

    [HttpPost("{id:guid}/archive")]
    [ProducesResponseType(
        typeof(ParentResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ParentResponseDto>> Archive(
        Guid id,
        CancellationToken cancellationToken)
    {
        var parent = await _sender.Send(
            new ArchiveParentCommand(id),
            cancellationToken);

        if (parent is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<ParentResponseDto>(parent);

        return Ok(response);
    }
}