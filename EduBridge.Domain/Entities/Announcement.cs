using EduBridge.Domain.Common.Base;
using EduBridge.Domain.Common.Enums;

namespace EduBridge.Domain.Entities;

public class Announcement : AuditableEntity
{
    public Guid SchoolId { get; private set; }
    public string Title { get; private set; }
    public string Body { get; private set; }
    public AnnouncementStatus Status { get; private set; }

    private Announcement()
    {
        Title = string.Empty;
        Body = string.Empty;
    }

    public Announcement(
        Guid schoolId,
        string title,
        string body)
    {
        if (schoolId == Guid.Empty)
            throw new ArgumentException(
                "School ID cannot be empty.",
                nameof(schoolId));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(
                "Announcement title cannot be empty.",
                nameof(title));

        if (string.IsNullOrWhiteSpace(body))
            throw new ArgumentException(
                "Announcement body cannot be empty.",
                nameof(body));

        SchoolId = schoolId;
        Title = title.Trim();
        Body = body.Trim();
        Status = AnnouncementStatus.Draft;
    }

    public void Update(
        string title,
        string body)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(
                "Announcement title cannot be empty.",
                nameof(title));

        if (string.IsNullOrWhiteSpace(body))
            throw new ArgumentException(
                "Announcement body cannot be empty.",
                nameof(body));

        Title = title.Trim();
        Body = body.Trim();

        MarkAsModified();
    }

    public void Publish()
    {
        if (Status == AnnouncementStatus.Archived)
            throw new InvalidOperationException(
                "Archived announcements cannot be published.");

        Status = AnnouncementStatus.Published;
        MarkAsModified();
    }

    public void Archive()
    {
        Status = AnnouncementStatus.Archived;
        MarkAsModified();
    }
}
