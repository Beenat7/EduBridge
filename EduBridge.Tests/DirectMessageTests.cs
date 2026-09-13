using EduBridge.Domain.Common.Enums;
using EduBridge.Domain.Entities;
using Xunit;

namespace EduBridge.Tests;

public class DirectMessageTests
{
    [Fact]
    public void Constructor_RequiresSchoolId()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new DirectMessage(
                Guid.Empty,
                Guid.NewGuid(),
                Guid.NewGuid(),
                DirectMessageParticipantType.Parent,
                Guid.NewGuid(),
                DirectMessageParticipantType.Teacher,
                "Hello"));

        Assert.Equal("School ID cannot be empty. (Parameter 'schoolId')", ex.Message);
    }

    [Fact]
    public void Constructor_RequiresSenderAndRecipientToDiffer()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new DirectMessage(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                DirectMessageParticipantType.Parent,
                Guid.NewGuid(),
                DirectMessageParticipantType.Parent,
                "Hello"));

        Assert.Equal("Sender and recipient must be different participants. (Parameter 'senderType')", ex.Message);
    }

    [Fact]
    public void MarkAsRead_SetsReadState()
    {
        var message = new DirectMessage(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            DirectMessageParticipantType.Parent,
            Guid.NewGuid(),
            DirectMessageParticipantType.Teacher,
            "Hello teacher");

        message.MarkAsRead();

        Assert.True(message.IsRead);
        Assert.NotNull(message.ReadAt);
    }
}
