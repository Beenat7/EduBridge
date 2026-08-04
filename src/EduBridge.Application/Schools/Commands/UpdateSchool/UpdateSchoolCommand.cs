using EduBridge.Application.Schools.DTOs;
using MediatR;

namespace EduBridge.Application.Schools.Commands.UpdateSchool;

public sealed record UpdateSchoolCommand(
    Guid Id,
    string Name,
    string Email,
    string PhoneNumber,
    string Address)
    : IRequest<SchoolDto?>;