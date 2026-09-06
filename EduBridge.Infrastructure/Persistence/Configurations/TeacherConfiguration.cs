using EduBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBridge.Infrastructure.Persistence.Configurations;

public sealed class TeacherConfiguration
    : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("Teachers");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.SchoolId)
            .IsRequired();

        builder.Property(t => t.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.MiddleName)
            .HasMaxLength(100);

        builder.Property(t => t.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(t => t.PhoneNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.EmployeeCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.HireDate)
            .IsRequired();

        builder.Property(t => t.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.HasIndex(t => new
        {
            t.SchoolId,
            t.EmployeeCode
        })
        .IsUnique();

        builder.HasOne<School>()
            .WithMany()
            .HasForeignKey(t => t.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}