namespace EduBridge.Application.Announcements.DTOs.Requests;

public sealed record UpdateAnnouncementRequestDto(
    string Title,
    string Body);