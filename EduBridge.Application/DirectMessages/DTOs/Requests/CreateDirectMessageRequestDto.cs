using EduBridge.Domain.Common.Enums;

namespace EduBridge.Application.DirectMessages.DTOs.Requests;

public sealed record CreateDirectMessageRequestDto(
    Guid SchoolId,
    Guid StudentId,
    Guid SenderId,
    DirectMessageParticipantType SenderType,
    Guid RecipientId,
    DirectMessageParticipantType RecipientType,
    string Body);
