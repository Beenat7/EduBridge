using EduBridge.Domain.Common.Base;
using EduBridge.Domain.Common.Enums;

namespace EduBridge.Domain.Entities;

public class Teacher : AuditableEntity
{
    public Guid SchoolId { get; private set; }
    public Guid? ClassId { get; private set; }
    public Class? Class { get; private set; }

    public string FirstName { get; private set; }
    public string MiddleName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string EmployeeCode { get; private set; }
    public DateOnly HireDate { get; private set; }
    public TeacherStatus Status { get; private set; }

    private Teacher()
    {
    }

    public Teacher(
        Guid schoolId,
        string firstName,
        string middleName,
        string lastName,
        string email,
        string phoneNumber,
        string employeeCode,
        DateOnly hireDate)
    {
        if (schoolId == Guid.Empty)
            throw new ArgumentException(
                "School ID cannot be empty.",
                nameof(schoolId));

        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException(
                "Teacher first name cannot be empty.",
                nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException(
                "Teacher last name cannot be empty.",
                nameof(lastName));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException(
                "Teacher email cannot be empty.",
                nameof(email));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException(
                "Teacher phone number cannot be empty.",
                nameof(phoneNumber));

        if (string.IsNullOrWhiteSpace(employeeCode))
            throw new ArgumentException(
                "Teacher employee code cannot be empty.",
                nameof(employeeCode));

        SchoolId = schoolId;
        FirstName = firstName.Trim();
        MiddleName = middleName?.Trim() ?? string.Empty;
        LastName = lastName.Trim();
        Email = email.Trim();
        PhoneNumber = phoneNumber.Trim();
        EmployeeCode = employeeCode.Trim();
        HireDate = hireDate;
        Status = TeacherStatus.Pending;
    }

    public void Update(
        string firstName,
        string middleName,
        string lastName,
        string email,
        string phoneNumber,
        string employeeCode,
        DateOnly hireDate)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException(
                "Teacher first name cannot be empty.",
                nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException(
                "Teacher last name cannot be empty.",
                nameof(lastName));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException(
                "Teacher email cannot be empty.",
                nameof(email));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException(
                "Teacher phone number cannot be empty.",
                nameof(phoneNumber));

        if (string.IsNullOrWhiteSpace(employeeCode))
            throw new ArgumentException(
                "Teacher employee code cannot be empty.",
                nameof(employeeCode));

        FirstName = firstName.Trim();
        MiddleName = middleName?.Trim() ?? string.Empty;
        LastName = lastName.Trim();
        Email = email.Trim();
        PhoneNumber = phoneNumber.Trim();
        EmployeeCode = employeeCode.Trim();
        HireDate = hireDate;

        MarkAsModified();
    }

    public void AssignToClass(Guid classId)
    {
        if (classId == Guid.Empty)
            throw new ArgumentException(
                "Class ID cannot be empty.",
                nameof(classId));

        ClassId = classId;

        MarkAsModified();
    }

    public void ChangeClass(Guid classId)
    {
        if (classId == Guid.Empty)
            throw new ArgumentException(
                "Class ID cannot be empty.",
                nameof(classId));

        ClassId = classId;

        MarkAsModified();
    }

    public void RemoveFromClass()
    {
        ClassId = null;

        MarkAsModified();
    }

    public void Activate()
    {
        if (Status == TeacherStatus.Archived)
            throw new InvalidOperationException(
                "Archived teachers cannot be activated.");

        Status = TeacherStatus.Active;
        MarkAsModified();
    }

    public void Deactivate()
    {
        if (Status == TeacherStatus.Archived)
            throw new InvalidOperationException(
                "Archived teachers cannot be deactivated.");

        Status = TeacherStatus.Inactive;
        MarkAsModified();
    }

    public void Archive()
    {
        Status = TeacherStatus.Archived;
        MarkAsModified();
    }
}