namespace EduBridge.Application.Announcements.DTOs.Responses;

public sealed record AnnouncementResponseDto(
    Guid Id,
    Guid SchoolId,
    string Title,
    string Body,
    string Status,
    DateTime CreatedAt,
    DateTime? LastModifiedAt);