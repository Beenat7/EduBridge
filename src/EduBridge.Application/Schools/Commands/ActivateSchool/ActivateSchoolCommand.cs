using EduBridge.Application.Schools.DTOs;
using MediatR;

namespace EduBridge.Application.Schools.Commands.ActivateSchool;

public sealed record ActivateSchoolCommand(Guid Id)
    : IRequest<SchoolDto?>;