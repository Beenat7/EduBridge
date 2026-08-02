using EduBridge.Application.Schools.DTOs;
using MediatR;

namespace EduBridge.Application.Schools.Commands.CreateSchool;

public sealed record CreateSchoolCommand(
    string Name,
    string Code,
    string Email,
    string PhoneNumber,
    string Address)
    : IRequest<SchoolDto>;