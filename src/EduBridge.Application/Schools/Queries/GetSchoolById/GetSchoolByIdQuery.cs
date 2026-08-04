using EduBridge.Application.Schools.DTOs;
using MediatR;

namespace EduBridge.Application.Schools.Queries.GetSchoolById;

public sealed record GetSchoolByIdQuery(Guid Id)
    : IRequest<SchoolDto?>;