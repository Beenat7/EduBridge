using EduBridge.Application.Interfaces;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.Students.Queries;

public sealed record GetStudentsQuery
    : IRequest<IReadOnlyList<Student>>;

public sealed class GetStudentsQueryHandler
    : IRequestHandler<GetStudentsQuery, IReadOnlyList<Student>>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentsQueryHandler(
        IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<IReadOnlyList<Student>> Handle(
        GetStudentsQuery request,
        CancellationToken cancellationToken)
    {
        return await _studentRepository.GetAllAsync(
            cancellationToken);
    }
}