using EduBridge.Application.Subjects.Commands;
using EduBridge.Application.Subjects.DTOs.Requests;
using EduBridge.Application.Subjects.DTOs.Responses;
using EduBridge.Application.Subjects.Queries;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduBridge.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public sealed class SubjectsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public SubjectsController(
        ISender sender,
        IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(
        typeof(SubjectResponseDto),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SubjectResponseDto>> Create(
        CreateSubjectRequestDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateSubjectCommand(
            request.SchoolId,
            request.Name,
            request.Code,
            request.Description);

        var subject = await _sender.Send(
            command,
            cancellationToken);

        var response =
            _mapper.Map<SubjectResponseDto>(subject);

        return CreatedAtAction(
            nameof(GetById),
            new { id = subject.Id },
            response);
    }

    [HttpGet]
    [ProducesResponseType(
        typeof(IReadOnlyList<SubjectResponseDto>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<SubjectResponseDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var subjects = await _sender.Send(
            new GetSubjectsQuery(),
            cancellationToken);

        var response =
            _mapper.Map<IReadOnlyList<SubjectResponseDto>>(subjects);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(
        typeof(SubjectResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SubjectResponseDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var subject = await _sender.Send(
            new GetSubjectByIdQuery(id),
            cancellationToken);

        if (subject is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<SubjectResponseDto>(subject);

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(
        typeof(SubjectResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SubjectResponseDto>> Update(
        Guid id,
        UpdateSubjectRequestDto request,
        CancellationToken cancellationToken)
    {
        var subject = await _sender.Send(
            new UpdateSubjectCommand(
                id,
                request.Name,
                request.Code,
                request.Description),
            cancellationToken);

        if (subject is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<SubjectResponseDto>(subject);

        return Ok(response);
    }

    [HttpPut("{id:guid}/activate")]
    [ProducesResponseType(
        typeof(SubjectResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SubjectResponseDto>> Activate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var subject = await _sender.Send(
            new ActivateSubjectCommand(id),
            cancellationToken);

        if (subject is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<SubjectResponseDto>(subject);

        return Ok(response);
    }

    [HttpPut("{id:guid}/deactivate")]
    [ProducesResponseType(
        typeof(SubjectResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SubjectResponseDto>> Deactivate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var subject = await _sender.Send(
            new DeactivateSubjectCommand(id),
            cancellationToken);

        if (subject is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<SubjectResponseDto>(subject);

        return Ok(response);
    }

    [HttpPost("{id:guid}/archive")]
    [ProducesResponseType(
        typeof(SubjectResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SubjectResponseDto>> Archive(
        Guid id,
        CancellationToken cancellationToken)
    {
        var subject = await _sender.Send(
            new ArchiveSubjectCommand(id),
            cancellationToken);

        if (subject is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<SubjectResponseDto>(subject);

        return Ok(response);
    }

    [HttpGet("{id:guid}/classes")]
    [ProducesResponseType(
        typeof(IReadOnlyList<SubjectClassResponseDto>),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<SubjectClassResponseDto>>> GetClasses(
        Guid id,
        CancellationToken cancellationToken)
    {
        var classes = await _sender.Send(
            new GetSubjectClassesQuery(id),
            cancellationToken);

        if (classes is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<IReadOnlyList<SubjectClassResponseDto>>(classes));
    }

    [HttpGet("{id:guid}/teachers")]
    [ProducesResponseType(
        typeof(IReadOnlyList<SubjectTeacherResponseDto>),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<SubjectTeacherResponseDto>>> GetTeachers(
        Guid id,
        CancellationToken cancellationToken)
    {
        var teachers = await _sender.Send(
            new GetSubjectTeachersQuery(id),
            cancellationToken);

        if (teachers is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<IReadOnlyList<SubjectTeacherResponseDto>>(teachers));
    }
}