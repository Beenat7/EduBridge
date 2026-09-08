using EduBridge.Application.Classes.Commands;
using EduBridge.Application.Classes.DTOs.Requests;
using EduBridge.Application.Classes.DTOs.Responses;
using EduBridge.Application.Classes.Queries;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduBridge.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public sealed class ClassesController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ClassesController(
        ISender sender,
        IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(
        typeof(ClassResponseDto),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClassResponseDto>> Create(
        CreateClassRequestDto request,
        CancellationToken cancellationToken)
    {
        var @class = await _sender.Send(
            new CreateClassCommand(
                request.SchoolId,
                request.Name,
                request.GradeLevel,
                request.Section),
            cancellationToken);

        var response =
            _mapper.Map<ClassResponseDto>(@class);

        return CreatedAtAction(
            nameof(GetById),
            new { id = @class.Id },
            response);
    }

    [HttpGet]
    [ProducesResponseType(
        typeof(IReadOnlyList<ClassResponseDto>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ClassResponseDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var classes = await _sender.Send(
            new GetClassesQuery(),
            cancellationToken);

        var response =
            _mapper.Map<IReadOnlyList<ClassResponseDto>>(classes);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(
        typeof(ClassResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClassResponseDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var @class = await _sender.Send(
            new GetClassByIdQuery(id),
            cancellationToken);

        if (@class is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<ClassResponseDto>(@class);

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(
        typeof(ClassResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClassResponseDto>> Update(
        Guid id,
        UpdateClassRequestDto request,
        CancellationToken cancellationToken)
    {
        var @class = await _sender.Send(
            new UpdateClassCommand(
                id,
                request.Name,
                request.GradeLevel,
                request.Section),
            cancellationToken);

        if (@class is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<ClassResponseDto>(@class);

        return Ok(response);
    }

    [HttpPut("{id:guid}/activate")]
    [ProducesResponseType(
        typeof(ClassResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClassResponseDto>> Activate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var @class = await _sender.Send(
            new ActivateClassCommand(id),
            cancellationToken);

        if (@class is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<ClassResponseDto>(@class);

        return Ok(response);
    }

    [HttpPut("{id:guid}/deactivate")]
    [ProducesResponseType(
        typeof(ClassResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClassResponseDto>> Deactivate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var @class = await _sender.Send(
            new DeactivateClassCommand(id),
            cancellationToken);

        if (@class is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<ClassResponseDto>(@class);

        return Ok(response);
    }

    [HttpPost("{id:guid}/archive")]
    [ProducesResponseType(
        typeof(ClassResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClassResponseDto>> Archive(
        Guid id,
        CancellationToken cancellationToken)
    {
        var @class = await _sender.Send(
            new ArchiveClassCommand(id),
            cancellationToken);

        if (@class is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<ClassResponseDto>(@class);

        return Ok(response);
    }

    [HttpPost("{id:guid}/subjects")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddSubject(
        Guid id,
        AddSubjectToClassRequestDto request,
        CancellationToken cancellationToken)
    {
        var classSubject = await _sender.Send(
            new AddSubjectToClassCommand(id, request.SubjectId),
            cancellationToken);

        if (classSubject is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{id:guid}/subjects")]
    [ProducesResponseType(
        typeof(IReadOnlyList<ClassSubjectResponseDto>),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<ClassSubjectResponseDto>>> GetSubjects(
        Guid id,
        CancellationToken cancellationToken)
    {
        var subjects = await _sender.Send(
            new GetClassSubjectsQuery(id),
            cancellationToken);

        if (subjects is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<IReadOnlyList<ClassSubjectResponseDto>>(subjects));
    }

    [HttpDelete("{id:guid}/subjects/{subjectId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveSubject(
        Guid id,
        Guid subjectId,
        CancellationToken cancellationToken)
    {
        var classSubject = await _sender.Send(
            new RemoveSubjectFromClassCommand(id, subjectId),
            cancellationToken);

        if (classSubject is null)
        {
            return NotFound();
        }

        return NoContent();
    }
}