using EduBridge.Application.Announcements.Commands;
using EduBridge.Application.Announcements.DTOs.Requests;
using EduBridge.Application.Announcements.DTOs.Responses;
using EduBridge.Application.Announcements.Queries;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduBridge.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public sealed class AnnouncementsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public AnnouncementsController(
        ISender sender,
        IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(
        typeof(AnnouncementResponseDto),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AnnouncementResponseDto>> Create(
        CreateAnnouncementRequestDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateAnnouncementCommand(
            request.SchoolId,
            request.Title,
            request.Body);

        var announcement = await _sender.Send(
            command,
            cancellationToken);

        var response = _mapper.Map<AnnouncementResponseDto>(announcement);

        return CreatedAtAction(
            nameof(GetById),
            new { id = announcement.Id },
            response);
    }

    [HttpGet]
    [ProducesResponseType(
        typeof(IReadOnlyList<AnnouncementResponseDto>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AnnouncementResponseDto>>> GetAll(
        [FromQuery] Guid? schoolId,
        CancellationToken cancellationToken)
    {
        var announcements = await _sender.Send(
            new GetAnnouncementsQuery(schoolId),
            cancellationToken);

        var response = _mapper.Map<IReadOnlyList<AnnouncementResponseDto>>(announcements);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(
        typeof(AnnouncementResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AnnouncementResponseDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var announcement = await _sender.Send(
            new GetAnnouncementByIdQuery(id),
            cancellationToken);

        if (announcement is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<AnnouncementResponseDto>(announcement);

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(
        typeof(AnnouncementResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AnnouncementResponseDto>> Update(
        Guid id,
        UpdateAnnouncementRequestDto request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateAnnouncementCommand(
            id,
            request.Title,
            request.Body);

        var announcement = await _sender.Send(
            command,
            cancellationToken);

        if (announcement is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<AnnouncementResponseDto>(announcement);

        return Ok(response);
    }

    [HttpPut("{id:guid}/publish")]
    [ProducesResponseType(
        typeof(AnnouncementResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AnnouncementResponseDto>> Publish(
        Guid id,
        CancellationToken cancellationToken)
    {
        var announcement = await _sender.Send(
            new PublishAnnouncementCommand(id),
            cancellationToken);

        if (announcement is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<AnnouncementResponseDto>(announcement);

        return Ok(response);
    }

    [HttpPost("{id:guid}/archive")]
    [ProducesResponseType(
        typeof(AnnouncementResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AnnouncementResponseDto>> Archive(
        Guid id,
        CancellationToken cancellationToken)
    {
        var announcement = await _sender.Send(
            new ArchiveAnnouncementCommand(id),
            cancellationToken);

        if (announcement is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<AnnouncementResponseDto>(announcement);

        return Ok(response);
    }
}
