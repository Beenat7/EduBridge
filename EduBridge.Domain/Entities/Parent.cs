using EduBridge.Domain.Common.Base;
using EduBridge.Domain.Common.Enums;

namespace EduBridge.Domain.Entities;

public class Parent : AuditableEntity
{
public Guid SchoolId { get; private set; }

public string FirstName { get; private set; }

public string MiddleName { get; private set; }

public string LastName { get; private set; }

public string Email { get; private set; }

public string PhoneNumber { get; private set; }

public ParentStatus Status { get; private set; }

private Parent()
{
}

public Parent(
    Guid schoolId,
    string firstName,
    string middleName,
    string lastName,
    string email,
    string phoneNumber)
{
    if (schoolId == Guid.Empty)
        throw new ArgumentException(
            "School ID cannot be empty.",
            nameof(schoolId));

    if (string.IsNullOrWhiteSpace(firstName))
        throw new ArgumentException(
            "Parent first name cannot be empty.",
            nameof(firstName));

    if (string.IsNullOrWhiteSpace(lastName))
        throw new ArgumentException(
            "Parent last name cannot be empty.",
            nameof(lastName));

    if (string.IsNullOrWhiteSpace(email))
        throw new ArgumentException(
            "Parent email cannot be empty.",
            nameof(email));

    if (string.IsNullOrWhiteSpace(phoneNumber))
        throw new ArgumentException(
            "Parent phone number cannot be empty.",
            nameof(phoneNumber));

    SchoolId = schoolId;

    FirstName = firstName.Trim();

    MiddleName = middleName?.Trim() ?? string.Empty;

    LastName = lastName.Trim();

    Email = email.Trim();

    PhoneNumber = phoneNumber.Trim();

    Status = ParentStatus.Pending;
}

public void Update(
    string firstName,
    string middleName,
    string lastName,
    string email,
    string phoneNumber)
{
    if (string.IsNullOrWhiteSpace(firstName))
        throw new ArgumentException(
            "Parent first name cannot be empty.",
            nameof(firstName));

    if (string.IsNullOrWhiteSpace(lastName))
        throw new ArgumentException(
            "Parent last name cannot be empty.",
            nameof(lastName));

    if (string.IsNullOrWhiteSpace(email))
        throw new ArgumentException(
            "Parent email cannot be empty.",
            nameof(email));

    if (string.IsNullOrWhiteSpace(phoneNumber))
        throw new ArgumentException(
            "Parent phone number cannot be empty.",
            nameof(phoneNumber));

    FirstName = firstName.Trim();

    MiddleName = middleName?.Trim() ?? string.Empty;

    LastName = lastName.Trim();

    Email = email.Trim();

    PhoneNumber = phoneNumber.Trim();

    MarkAsModified();
}

public void Activate()
{
    if (Status == ParentStatus.Archived)
        throw new InvalidOperationException(
            "Archived parents cannot be activated.");

    Status = ParentStatus.Active;

    MarkAsModified();
}

public void Deactivate()
{
    if (Status == ParentStatus.Archived)
        throw new InvalidOperationException(
            "Archived parents cannot be deactivated.");

    Status = ParentStatus.Inactive;

    MarkAsModified();
}

public void Archive()
{
    Status = ParentStatus.Archived;

    MarkAsModified();
}

}
