using EduBridge.Application.DirectMessages.Commands;
using EduBridge.Application.DirectMessages.DTOs.Requests;
using EduBridge.Application.DirectMessages.DTOs.Responses;
using EduBridge.Application.DirectMessages.Queries;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduBridge.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public sealed class DirectMessagesController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public DirectMessagesController(
        ISender sender,
        IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(
        typeof(DirectMessageResponseDto),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DirectMessageResponseDto>> Create(
        CreateDirectMessageRequestDto request,
        CancellationToken cancellationToken)
    {
        var message = await _sender.Send(
            new CreateDirectMessageCommand(
                request.SchoolId,
                request.StudentId,
                request.SenderId,
                request.SenderType,
                request.RecipientId,
                request.RecipientType,
                request.Body),
            cancellationToken);

        var response = _mapper.Map<DirectMessageResponseDto>(message);

        return CreatedAtAction(
            nameof(GetById),
            new { id = message.Id },
            response);
    }

    [HttpGet]
    [ProducesResponseType(
        typeof(IReadOnlyList<DirectMessageResponseDto>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<DirectMessageResponseDto>>> GetAll(
        [FromQuery] Guid? schoolId,
        [FromQuery] Guid? studentId,
        [FromQuery] Guid? participantId,
        [FromQuery] string? participantType,
        CancellationToken cancellationToken)
    {
        Domain.Common.Enums.DirectMessageParticipantType? participant =
            participantType is null
                ? null
                : Enum.Parse<Domain.Common.Enums.DirectMessageParticipantType>(
                    participantType,
                    ignoreCase: true);

        var messages = await _sender.Send(
            new GetDirectMessagesQuery(
                schoolId,
                studentId,
                participantId,
                participant),
            cancellationToken);

        var response = _mapper.Map<IReadOnlyList<DirectMessageResponseDto>>(messages);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(
        typeof(DirectMessageResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DirectMessageResponseDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var message = await _sender.Send(
            new GetDirectMessageByIdQuery(id),
            cancellationToken);

        if (message is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<DirectMessageResponseDto>(message);

        return Ok(response);
    }

    [HttpPut("{id:guid}/read")]
    [ProducesResponseType(
        typeof(DirectMessageResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DirectMessageResponseDto>> MarkAsRead(
        Guid id,
        CancellationToken cancellationToken)
    {
        var message = await _sender.Send(
            new MarkDirectMessageAsReadCommand(id),
            cancellationToken);

        if (message is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<DirectMessageResponseDto>(message);

        return Ok(response);
    }
}
