using EduBridge.Domain.Common.Base;
using EduBridge.Domain.Common.Enums;

namespace EduBridge.Domain.Entities;

public class Student : AuditableEntity
{
public string FirstName { get; private set; }
public string MiddleName { get; private set; }
public string LastName { get; private set; }
public string StudentCode { get; private set; }
public DateTime DateOfBirth { get; private set; }
public Gender Gender { get; private set; }
public Guid SchoolId { get; private set; }
public Guid? ParentId { get; private set; }
public string Grade { get; private set; }
public StudentStatus Status { get; private set; }

public Student(
    string firstName,
    string middleName,
    string lastName,
    string studentCode,
    DateTime dateOfBirth,
    Gender gender,
    Guid schoolId,
    string grade)
{
    if (string.IsNullOrWhiteSpace(firstName))
        throw new ArgumentException(
            "Student first name cannot be empty.",
            nameof(firstName));

    if (string.IsNullOrWhiteSpace(lastName))
        throw new ArgumentException(
            "Student last name cannot be empty.",
            nameof(lastName));

    if (string.IsNullOrWhiteSpace(studentCode))
        throw new ArgumentException(
            "Student code cannot be empty.",
            nameof(studentCode));

    if (schoolId == Guid.Empty)
        throw new ArgumentException(
            "School ID cannot be empty.",
            nameof(schoolId));

    if (string.IsNullOrWhiteSpace(grade))
        throw new ArgumentException(
            "Student grade cannot be empty.",
            nameof(grade));

    FirstName = firstName.Trim();

    MiddleName = middleName?.Trim() ?? string.Empty;

    LastName = lastName.Trim();

    StudentCode = studentCode.Trim()
        .ToUpperInvariant();

    DateOfBirth = dateOfBirth;

    Gender = gender;

    SchoolId = schoolId;

    Grade = grade.Trim();

    Status = StudentStatus.Active;
}

public void UpdatePersonalInformation(
    string firstName,
    string middleName,
    string lastName,
    DateTime dateOfBirth,
    Gender gender)
{
    if (string.IsNullOrWhiteSpace(firstName))
        throw new ArgumentException(
            "Student first name cannot be empty.",
            nameof(firstName));

    if (string.IsNullOrWhiteSpace(lastName))
        throw new ArgumentException(
            "Student last name cannot be empty.",
            nameof(lastName));

    FirstName = firstName.Trim();

    MiddleName = middleName?.Trim() ?? string.Empty;

    LastName = lastName.Trim();

    DateOfBirth = dateOfBirth;

    Gender = gender;

    MarkAsModified();
}

public void UpdateGrade(string grade)
{
    if (string.IsNullOrWhiteSpace(grade))
        throw new ArgumentException(
            "Student grade cannot be empty.",
            nameof(grade));

    Grade = grade.Trim();

    MarkAsModified();
}

public void AssignParent(Guid parentId)
{
    if (parentId == Guid.Empty)
        throw new ArgumentException(
            "Parent ID cannot be empty.",
            nameof(parentId));

    ParentId = parentId;

    MarkAsModified();
}

public void Deactivate()
{
    if (Status == StudentStatus.Archived)
        throw new InvalidOperationException(
            "Archived students cannot be deactivated.");

    Status = StudentStatus.Inactive;

    MarkAsModified();
}

public void Activate()
{
    if (Status == StudentStatus.Archived)
        throw new InvalidOperationException(
            "Archived students cannot be activated.");

    Status = StudentStatus.Active;

    MarkAsModified();
}

public void Archive()
{
    Status = StudentStatus.Archived;

    MarkAsModified();
}

}
