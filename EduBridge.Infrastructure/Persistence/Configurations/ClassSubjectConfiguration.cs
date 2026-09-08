using EduBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBridge.Infrastructure.Persistence.Configurations;

public sealed class ClassSubjectConfiguration
    : IEntityTypeConfiguration<ClassSubject>
{
    public void Configure(EntityTypeBuilder<ClassSubject> builder)
    {
        builder.ToTable("ClassSubjects");

        builder.HasKey(cs => cs.Id);

        builder.Property(cs => cs.ClassId)
            .IsRequired();

        builder.Property(cs => cs.SubjectId)
            .IsRequired();

        builder.HasIndex(cs => new
        {
            cs.ClassId,
            cs.SubjectId
        })
        .IsUnique();

        builder.HasOne<Class>()
            .WithMany()
            .HasForeignKey(cs => cs.ClassId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Subject>()
            .WithMany()
            .HasForeignKey(cs => cs.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
