using EduBridge.Domain.Common.Base;
using EduBridge.Domain.Common.Enums;

namespace EduBridge.Domain.Entities;

public class DirectMessage : AuditableEntity
{
    public Guid SchoolId { get; private set; }
    public Guid StudentId { get; private set; }
    public Guid SenderId { get; private set; }
    public DirectMessageParticipantType SenderType { get; private set; }
    public Guid RecipientId { get; private set; }
    public DirectMessageParticipantType RecipientType { get; private set; }
    public string Body { get; private set; }
    public bool IsRead { get; private set; }
    public DateTime? ReadAt { get; private set; }

    private DirectMessage()
    {
        Body = string.Empty;
    }

    public DirectMessage(
        Guid schoolId,
        Guid studentId,
        Guid senderId,
        DirectMessageParticipantType senderType,
        Guid recipientId,
        DirectMessageParticipantType recipientType,
        string body)
    {
        if (schoolId == Guid.Empty)
            throw new ArgumentException(
                "School ID cannot be empty.",
                nameof(schoolId));

        if (studentId == Guid.Empty)
            throw new ArgumentException(
                "Student ID cannot be empty.",
                nameof(studentId));

        if (senderId == Guid.Empty)
            throw new ArgumentException(
                "Sender ID cannot be empty.",
                nameof(senderId));

        if (recipientId == Guid.Empty)
            throw new ArgumentException(
                "Recipient ID cannot be empty.",
                nameof(recipientId));

        if (string.IsNullOrWhiteSpace(body))
            throw new ArgumentException(
                "Message body cannot be empty.",
                nameof(body));

        if (senderType == recipientType)
            throw new ArgumentException(
                "Sender and recipient must be different participants.",
                nameof(senderType));

        SchoolId = schoolId;
        StudentId = studentId;
        SenderId = senderId;
        SenderType = senderType;
        RecipientId = recipientId;
        RecipientType = recipientType;
        Body = body.Trim();
        IsRead = false;
        ReadAt = null;
    }

    public void UpdateBody(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
            throw new ArgumentException(
                "Message body cannot be empty.",
                nameof(body));

        Body = body.Trim();
        MarkAsModified();
    }

    public void MarkAsRead()
    {
        IsRead = true;
        ReadAt = DateTime.UtcNow;
        MarkAsModified();
    }
}
