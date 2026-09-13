using EduBridge.Domain.Common.Enums;

namespace EduBridge.Application.DirectMessages.DTOs.Responses;

public sealed record DirectMessageResponseDto(
    Guid Id,
    Guid SchoolId,
    Guid StudentId,
    Guid SenderId,
    string SenderType,
    Guid RecipientId,
    string RecipientType,
    string Body,
    bool IsRead,
    DateTime? ReadAt,
    DateTime CreatedAt,
    DateTime? LastModifiedAt);
