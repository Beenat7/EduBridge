using EduBridge.Domain.Common.Base;

namespace EduBridge.Domain.Entities;

public class ClassSubject : AuditableEntity
{
    public Guid ClassId { get; private set; }
    public Guid SubjectId { get; private set; }

    private ClassSubject()
    {
    }

    public ClassSubject(Guid classId, Guid subjectId)
    {
        if (classId == Guid.Empty)
            throw new ArgumentException(
                "Class ID cannot be empty.",
                nameof(classId));

        if (subjectId == Guid.Empty)
            throw new ArgumentException(
                "Subject ID cannot be empty.",
                nameof(subjectId));

        ClassId = classId;
        SubjectId = subjectId;
    }
}
