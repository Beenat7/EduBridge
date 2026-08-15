using EduBridge.Application.Students.Commands;
using EduBridge.Application.Students.DTOs.Requests;
using EduBridge.Application.Students.DTOs.Responses;
using EduBridge.Application.Students.Queries;

using MapsterMapper;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace EduBridge.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class StudentsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public StudentsController(
        ISender sender,
        IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(
        typeof(StudentResponseDto),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StudentResponseDto>> Create(
        CreateStudentCommand command,
        CancellationToken cancellationToken)
    {
        var student = await _sender.Send(
            command,
            cancellationToken);

        var response = _mapper.Map<StudentResponseDto>(student);

        return CreatedAtAction(
            nameof(GetById),
            new { id = student.Id },
            response);
    }

    [HttpGet]
    [ProducesResponseType(
        typeof(IReadOnlyList<StudentResponseDto>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<StudentResponseDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var students = await _sender.Send(
            new GetStudentsQuery(),
            cancellationToken);

        var response =
            _mapper.Map<IReadOnlyList<StudentResponseDto>>(students);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(
        typeof(StudentResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StudentResponseDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var student = await _sender.Send(
            new GetStudentByIdQuery(id),
            cancellationToken);

        if (student is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<StudentResponseDto>(student);

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(
        typeof(StudentResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StudentResponseDto>> Update(
        Guid id,
        UpdateStudentRequestDto request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateStudentCommand(
            id,
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.DateOfBirth,
            request.Gender,
            request.Grade);

        var student = await _sender.Send(
            command,
            cancellationToken);

        if (student is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<StudentResponseDto>(student);

        return Ok(response);
    }

    [HttpPut("{id:guid}/activate")]
    [ProducesResponseType(
        typeof(StudentResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StudentResponseDto>> Activate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var student = await _sender.Send(
            new ActivateStudentCommand(id),
            cancellationToken);

        if (student is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<StudentResponseDto>(student);

        return Ok(response);
    }

    [HttpPut("{id:guid}/deactivate")]
    [ProducesResponseType(
        typeof(StudentResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StudentResponseDto>> Deactivate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var student = await _sender.Send(
            new DeactivateStudentCommand(id),
            cancellationToken);

        if (student is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<StudentResponseDto>(student);

        return Ok(response);
    }

    [HttpPost("{id:guid}/archive")]
    [ProducesResponseType(
        typeof(StudentResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StudentResponseDto>> Archive(
        Guid id,
        CancellationToken cancellationToken)
    {
        var student = await _sender.Send(
            new ArchiveStudentCommand(id),
            cancellationToken);

        if (student is null)
        {
            return NotFound();
        }

        var response =
            _mapper.Map<StudentResponseDto>(student);

        return Ok(response);
    }
}