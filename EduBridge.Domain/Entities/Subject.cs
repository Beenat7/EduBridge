using EduBridge.Domain.Common.Base;
using EduBridge.Domain.Common.Enums;

namespace EduBridge.Domain.Entities;

public class Subject : AuditableEntity
{
    public Guid SchoolId { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public SubjectStatus Status { get; private set; }

    private Subject()
    {
        Name = string.Empty;
        Code = string.Empty;
        Description = string.Empty;
    }

    public Subject(
        Guid schoolId,
        string name,
        string code,
        string description)
    {
        if (schoolId == Guid.Empty)
            throw new ArgumentException(
                "School ID cannot be empty.",
                nameof(schoolId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Subject name cannot be empty.",
                nameof(name));

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException(
                "Subject code cannot be empty.",
                nameof(code));

        SchoolId = schoolId;
        Name = name.Trim();
        Code = code.Trim();
        Description = description?.Trim() ?? string.Empty;
        Status = SubjectStatus.Active;
    }

    public void Update(
        string name,
        string code,
        string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Subject name cannot be empty.",
                nameof(name));

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException(
                "Subject code cannot be empty.",
                nameof(code));

        Name = name.Trim();
        Code = code.Trim();
        Description = description?.Trim() ?? string.Empty;

        MarkAsModified();
    }

    public void Activate()
    {
        if (Status == SubjectStatus.Archived)
            throw new InvalidOperationException(
                "Archived subjects cannot be activated.");

        Status = SubjectStatus.Active;
        MarkAsModified();
    }

    public void Deactivate()
    {
        if (Status == SubjectStatus.Archived)
            throw new InvalidOperationException(
                "Archived subjects cannot be deactivated.");

        Status = SubjectStatus.Inactive;
        MarkAsModified();
    }

    public void Archive()
    {
        Status = SubjectStatus.Archived;
        MarkAsModified();
    }
}