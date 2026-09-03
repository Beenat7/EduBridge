using EduBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBridge.Infrastructure.Persistence.Configurations;

public sealed class StudentConfiguration
    : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.MiddleName)
            .HasMaxLength(100);

        builder.Property(s => s.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.StudentCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(s => s.StudentCode)
            .IsUnique();

        builder.Property(s => s.DateOfBirth)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(s => s.Gender)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(s => s.SchoolId)
            .IsRequired();

        builder.Property(s => s.ParentId)
                   .IsRequired(false);

        builder.Property(s => s.Grade)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.HasOne<School>()
            .WithMany()
            .HasForeignKey(s => s.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Parent>()
        .WithMany()
        .HasForeignKey(s => s.ParentId)
        .OnDelete(DeleteBehavior.Restrict);    
    }
}