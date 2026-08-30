using EduBridge.Domain.Common.Enums;
namespace EduBridge.Domain.Entities;
using EduBridge.Domain.Common.Base;

public class Parent : AuditableEntity
{
    public string FirstName { get; private set; }
    public string MiddleName { get; private set; }
    public string LastName { get; private set; }

    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Address { get; private set; }

    public ParentStatus Status { get; private set; }

    private Parent()
    {
    }

    public Parent(
        string firstName,
        string middleName,
        string lastName,
        string email,
        string phoneNumber,
        string address)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException(
                "Parent first name cannot be empty.",
                nameof(firstName));

        if (string.IsNullOrWhiteSpace(middleName))
            throw new ArgumentException(
                "Parent middle name cannot be empty.",
                nameof(middleName));

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

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException(
                "Parent address cannot be empty.",
                nameof(address));

        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;

        Status = ParentStatus.Pending;
    }

    public void Update(
        string firstName,
        string middleName,
        string lastName,
        string email,
        string phoneNumber,
        string address)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException(
                "Parent first name cannot be empty.",
                nameof(firstName));

        if (string.IsNullOrWhiteSpace(middleName))
            throw new ArgumentException(
                "Parent middle name cannot be empty.",
                nameof(middleName));

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

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException(
                "Parent address cannot be empty.",
                nameof(address));

        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
    }

    public void Approve()
    {
        Status = ParentStatus.Active;
    }

    public void Reject()
    {
        Status = ParentStatus.Rejected;
    }

    public void Activate()
    {
        Status = ParentStatus.Active;
    }

    public void Deactivate()
    {
        Status = ParentStatus.Inactive;
    }

    public void Archive()
    {
        Status = ParentStatus.Archived;
    }
}