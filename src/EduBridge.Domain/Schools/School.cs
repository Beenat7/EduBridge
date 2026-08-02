using EduBridge.Domain.Common.Base;
using EduBridge.Domain.Common.Enums;

namespace EduBridge.Domain.Schools;

public class School : AggregateRoot
{
    public string Name { get; private set; }

    public string Code { get; private set; }

    public string Email { get; private set; }

    public string PhoneNumber { get; private set; }

    public string Address { get; private set; }

    public SchoolStatus Status { get; private set; }


    public School(
        string name,
        string code,
        string email,
        string phoneNumber,
        string address)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "School name cannot be empty.",
                nameof(name));

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException(
                "School code cannot be empty.",
                nameof(code));


        Name = name.Trim();

        Code = code.Trim()
                   .ToUpperInvariant();

        Email = email.Trim();

        PhoneNumber = phoneNumber.Trim();

        Address = address.Trim();

        Status = SchoolStatus.Active;
    }


    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException(
                "School name cannot be empty.",
                nameof(newName));

        Name = newName.Trim();

        MarkAsModified();
    }


    public void UpdateContactInformation(
        string email,
        string phoneNumber,
        string address)
    {
        Email = email.Trim();

        PhoneNumber = phoneNumber.Trim();

        Address = address.Trim();

        MarkAsModified();
    }


    public void Deactivate()
    {
        if (Status == SchoolStatus.Archived)
            throw new InvalidOperationException(
                "Archived schools cannot be deactivated.");

        Status = SchoolStatus.Inactive;

        MarkAsModified();
    }


    public void Activate()
    {
        if (Status == SchoolStatus.Archived)
            throw new InvalidOperationException(
                "Archived schools cannot be activated.");

        Status = SchoolStatus.Active;

        MarkAsModified();
    }


    public void Archive()
    {
        Status = SchoolStatus.Archived;

        MarkAsModified();
    }
}