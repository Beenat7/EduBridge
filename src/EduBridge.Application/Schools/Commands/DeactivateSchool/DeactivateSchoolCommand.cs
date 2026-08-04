using EduBridge.Application.Schools.DTOs;
using MediatR;

namespace EduBridge.Application.Schools.Commands.DeactivateSchool;

public sealed record DeactivateSchoolCommand(
    Guid Id
) : IRequest<SchoolDto?>;