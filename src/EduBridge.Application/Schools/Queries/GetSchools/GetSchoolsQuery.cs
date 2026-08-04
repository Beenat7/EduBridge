using EduBridge.Application.Schools.DTOs;
using MediatR;

namespace EduBridge.Application.Schools.Queries.GetSchools;

public sealed record GetSchoolsQuery
    : IRequest<IReadOnlyList<SchoolDto>>;