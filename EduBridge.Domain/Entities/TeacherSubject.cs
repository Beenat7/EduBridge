using EduBridge.Domain.Common.Base;

namespace EduBridge.Domain.Entities;

public class TeacherSubject : AuditableEntity
{
    public Guid TeacherId { get; private set; }
    public Guid SubjectId { get; private set; }

    private TeacherSubject()
    {
    }

    public TeacherSubject(Guid teacherId, Guid subjectId)
    {
        if (teacherId == Guid.Empty)
            throw new ArgumentException(
                "Teacher ID cannot be empty.",
                nameof(teacherId));

        if (subjectId == Guid.Empty)
            throw new ArgumentException(
                "Subject ID cannot be empty.",
                nameof(subjectId));

        TeacherId = teacherId;
        SubjectId = subjectId;
    }
}
