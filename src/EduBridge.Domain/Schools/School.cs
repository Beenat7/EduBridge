using EduBridge.Domain.Common.Base;
using EduBridge.Domain.Common.Enums;

namespace EduBridge.Domain.Schools;

public class School : AggregateRoot
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public SchoolStatus Status { get; private set; }
    public School(string name, string code)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("School name cannot be empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("School code cannot be empty.", nameof(code));

        Name = name.Trim();
        Code = code.Trim().ToUpperInvariant();
        Status = SchoolStatus.Active;
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("School name cannot be empty.", nameof(newName));

        Name = newName.Trim();
        MarkAsModified();
    }

    public void Archive()
    {
        Status = SchoolStatus.Archived;
        MarkAsModified();
    }

    public void Deactivate()
    {
        Status = SchoolStatus.Inactive;
        MarkAsModified();
    }

    public void Activate()
    {
        Status = SchoolStatus.Active;
        MarkAsModified();
    }
}