using EduBridge.Application.Schools.DTOs;
using MediatR;

namespace EduBridge.Application.Schools.Commands.ArchiveSchool;

public sealed record ArchiveSchoolCommand(Guid Id)
    : IRequest<SchoolDto?>;