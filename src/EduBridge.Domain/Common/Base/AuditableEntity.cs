namespace EduBridge.Domain.Common.Base;

public abstract class AuditableEntity : Entity
{
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? LastModifiedAt { get; private set; }
    protected void MarkAsModified()
    {
        LastModifiedAt = DateTime.UtcNow;
    }
}