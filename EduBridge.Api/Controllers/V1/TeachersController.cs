using EduBridge.Application.Teachers.Commands;
using EduBridge.Application.Teachers.DTOs.Requests;
using EduBridge.Application.Teachers.DTOs.Responses;
using EduBridge.Application.Teachers.Queries;

using MapsterMapper;
using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduBridge.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public sealed class TeachersController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public TeachersController(
        ISender sender,
        IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(
        typeof(TeacherResponseDto),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TeacherResponseDto>> Create(
        CreateTeacherRequestDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateTeacherCommand(
            request.SchoolId,
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.EmployeeCode,
            request.HireDate);

        var teacher = await _sender.Send(
            command,
            cancellationToken);

        var response =
            _mapper.Map<TeacherResponseDto>(teacher);

        return CreatedAtAction(
            nameof(GetById),
            new { id = teacher.Id },
            response);
    }

    [HttpGet]
    [ProducesResponseType(
        typeof(IReadOnlyList<TeacherResponseDto>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TeacherResponseDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var teachers = await _sender.Send(
            new GetTeachersQuery(),
            cancellationToken);

        var response =
            _mapper.Map<IReadOnlyList<TeacherResponseDto>>(teachers);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(
        typeof(TeacherResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TeacherResponseDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var teacher = await _sender.Send(
            new GetTeacherByIdQuery(id),
            cancellationToken);

        if (teacher is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<TeacherResponseDto>(teacher);

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(
        typeof(TeacherResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TeacherResponseDto>> Update(
        Guid id,
        UpdateTeacherRequestDto request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateTeacherCommand(
            id,
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.EmployeeCode,
            request.HireDate);

        var teacher = await _sender.Send(
            command,
            cancellationToken);

        if (teacher is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<TeacherResponseDto>(teacher);

        return Ok(response);
    }

    [HttpPut("{id:guid}/activate")]
    [ProducesResponseType(
        typeof(TeacherResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TeacherResponseDto>> Activate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var teacher = await _sender.Send(
            new ActivateTeacherCommand(id),
            cancellationToken);

        if (teacher is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<TeacherResponseDto>(teacher);

        return Ok(response);
    }

    [HttpPut("{id:guid}/deactivate")]
    [ProducesResponseType(
        typeof(TeacherResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TeacherResponseDto>> Deactivate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var teacher = await _sender.Send(
            new DeactivateTeacherCommand(id),
            cancellationToken);

        if (teacher is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<TeacherResponseDto>(teacher);

        return Ok(response);
    }

    [HttpPost("{id:guid}/archive")]
    [ProducesResponseType(
        typeof(TeacherResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TeacherResponseDto>> Archive(
        Guid id,
        CancellationToken cancellationToken)
    {
        var teacher = await _sender.Send(
            new ArchiveTeacherCommand(id),
            cancellationToken);

        if (teacher is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<TeacherResponseDto>(teacher);

        return Ok(response);
    }

    [HttpPost("{id:guid}/class")]
    [ProducesResponseType(
        typeof(TeacherResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TeacherResponseDto>> AssignToClass(
        Guid id,
        ClassAssignmentRequestDto request,
        CancellationToken cancellationToken)
    {
        var teacher = await _sender.Send(
            new AssignTeacherToClassCommand(id, request.ClassId),
            cancellationToken);

        if (teacher is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<TeacherResponseDto>(teacher));
    }

    [HttpPut("{id:guid}/class")]
    [ProducesResponseType(
        typeof(TeacherResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TeacherResponseDto>> ChangeClass(
        Guid id,
        ClassAssignmentRequestDto request,
        CancellationToken cancellationToken)
    {
        var teacher = await _sender.Send(
            new ChangeTeacherClassCommand(id, request.ClassId),
            cancellationToken);

        if (teacher is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<TeacherResponseDto>(teacher));
    }

    [HttpDelete("{id:guid}/class")]
    [ProducesResponseType(
        typeof(TeacherResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TeacherResponseDto>> RemoveFromClass(
        Guid id,
        CancellationToken cancellationToken)
    {
        var teacher = await _sender.Send(
            new RemoveTeacherFromClassCommand(id),
            cancellationToken);

        if (teacher is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<TeacherResponseDto>(teacher));
    }
}