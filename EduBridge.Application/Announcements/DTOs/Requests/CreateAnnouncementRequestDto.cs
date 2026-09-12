namespace EduBridge.Application.Announcements.DTOs.Requests;

public sealed record CreateAnnouncementRequestDto(
    Guid SchoolId,
    string Title,
    string Body);