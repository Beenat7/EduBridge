using EduBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBridge.Infrastructure.Persistence.Configurations;

public sealed class TeacherSubjectConfiguration
    : IEntityTypeConfiguration<TeacherSubject>
{
    public void Configure(EntityTypeBuilder<TeacherSubject> builder)
    {
        builder.ToTable("TeacherSubjects");

        builder.HasKey(ts => ts.Id);

        builder.Property(ts => ts.TeacherId)
            .IsRequired();

        builder.Property(ts => ts.SubjectId)
            .IsRequired();

        builder.HasIndex(ts => new
        {
            ts.TeacherId,
            ts.SubjectId
        })
        .IsUnique();

        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(ts => ts.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Subject>()
            .WithMany()
            .HasForeignKey(ts => ts.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
