using EduBridge.Domain.Common.Base;
using EduBridge.Domain.Common.Enums;

namespace EduBridge.Domain.Entities;

public class Class : AuditableEntity
{
    public Guid SchoolId { get; private set; }
    public string Name { get; private set; }
    public GradeLevel GradeLevel { get; private set; }
    public string Section { get; private set; }
    public ClassStatus Status { get; private set; }

    private Class()
    {
        Name = string.Empty;
        Section = string.Empty;
    }

    public Class(
        Guid schoolId,
        string name,
        GradeLevel gradeLevel,
        string section)
    {
        ValidateSchoolId(schoolId);
        ValidateName(name);
        ValidateGradeLevel(gradeLevel);
        ValidateSection(section);

        SchoolId = schoolId;
        Name = name.Trim();
        GradeLevel = gradeLevel;
        Section = section.Trim();
        Status = ClassStatus.Active;
    }

    public void Update(
        string name,
        GradeLevel gradeLevel,
        string section)
    {
        ValidateName(name);
        ValidateGradeLevel(gradeLevel);
        ValidateSection(section);

        Name = name.Trim();
        GradeLevel = gradeLevel;
        Section = section.Trim();

        MarkAsModified();
    }

    public void Activate()
    {
        if (Status == ClassStatus.Archived)
            throw new InvalidOperationException(
                "Archived classes cannot be activated.");

        Status = ClassStatus.Active;
        MarkAsModified();
    }

    public void Deactivate()
    {
        if (Status == ClassStatus.Archived)
            throw new InvalidOperationException(
                "Archived classes cannot be deactivated.");

        Status = ClassStatus.Inactive;
        MarkAsModified();
    }

    public void Archive()
    {
        Status = ClassStatus.Archived;
        MarkAsModified();
    }

    private static void ValidateSchoolId(Guid schoolId)
    {
        if (schoolId == Guid.Empty)
            throw new ArgumentException(
                "School ID cannot be empty.",
                nameof(schoolId));
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Class name cannot be empty.",
                nameof(name));
    }

    private static void ValidateGradeLevel(GradeLevel gradeLevel)
    {
        if (!Enum.IsDefined(gradeLevel))
            throw new ArgumentOutOfRangeException(
                nameof(gradeLevel),
                "Grade level is invalid.");
    }

    private static void ValidateSection(string section)
    {
        if (string.IsNullOrWhiteSpace(section))
            throw new ArgumentException(
                "Class section cannot be empty.",
                nameof(section));
    }
}