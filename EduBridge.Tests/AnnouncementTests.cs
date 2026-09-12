using EduBridge.Domain.Common.Enums;
using EduBridge.Domain.Entities;
using Xunit;

namespace EduBridge.Tests;

public class AnnouncementTests
{
    [Fact]
    public void Constructor_RequiresSchoolId()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new Announcement(
                Guid.Empty,
                "Title",
                "Body"));

        Assert.Equal("School ID cannot be empty. (Parameter 'schoolId')", ex.Message);
    }

    [Fact]
    public void Publish_TransitionsToPublished()
    {
        var announcement = new Announcement(
            Guid.NewGuid(),
            "Title",
            "Body");

        announcement.Publish();

        Assert.Equal(AnnouncementStatus.Published, announcement.Status);
    }

    [Fact]
    public void Archive_TransitionFromPublished_IsAllowed()
    {
        var announcement = new Announcement(
            Guid.NewGuid(),
            "Title",
            "Body");

        announcement.Publish();
        announcement.Archive();

        Assert.Equal(AnnouncementStatus.Archived, announcement.Status);
    }
}
