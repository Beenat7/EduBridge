using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Students.Queries;

public sealed record GetStudentByIdQuery(Guid Id)
    : IRequest<Student?>;

public sealed class GetStudentByIdQueryHandler
    : IRequestHandler<GetStudentByIdQuery, Student?>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentByIdQueryHandler(
        IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Student?> Handle(
        GetStudentByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _studentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);
    }
}