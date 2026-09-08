using EduBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBridge.Infrastructure.Persistence.Configurations;

public sealed class ClassConfiguration
    : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.ToTable("Classes");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.SchoolId)
            .IsRequired();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.GradeLevel)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(c => c.Section)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.HasIndex(c => new
        {
            c.SchoolId,
            c.Name
        })
        .IsUnique();

        builder.HasOne<School>()
            .WithMany()
            .HasForeignKey(c => c.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}