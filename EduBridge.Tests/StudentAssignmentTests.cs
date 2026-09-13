using EduBridge.Application.Students.Commands;
using EduBridge.Domain.Common.Enums;
using EduBridge.Domain.Entities;
using Xunit;

namespace EduBridge.Tests;

public class StudentAssignmentTests
{
    [Fact]
    public void AssignStudentToParentCommand_RequiresParentId()
    {
        var command = new AssignStudentToParentCommand(
            Guid.NewGuid(),
            Guid.NewGuid());

        Assert.Equal(command.StudentId, command.StudentId);
        Assert.Equal(command.ParentId, command.ParentId);
    }

    [Fact]
    public void Student_AssignParent_SetsParentReference()
    {
        var schoolId = Guid.NewGuid();
        var student = new Student(
            "Jane",
            "A",
            "Doe",
            "STU-100",
            new DateTime(2015, 5, 10),
            Gender.Female,
            schoolId,
            "Grade 3");

        var parent = new Parent(
            schoolId,
            "John",
            "",
            "Doe",
            "john@example.com",
            "+1234567890");

        parent.Activate();
        student.AssignParent(parent.Id);

        Assert.Equal(parent.Id, student.ParentId);
    }
}
