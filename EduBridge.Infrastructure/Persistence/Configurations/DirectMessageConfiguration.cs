using EduBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBridge.Infrastructure.Persistence.Configurations;

public sealed class DirectMessageConfiguration : IEntityTypeConfiguration<DirectMessage>
{
    public void Configure(EntityTypeBuilder<DirectMessage> builder)
    {
        builder.ToTable("DirectMessages");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.SchoolId)
            .IsRequired();

        builder.Property(d => d.StudentId)
            .IsRequired();

        builder.Property(d => d.SenderId)
            .IsRequired();

        builder.Property(d => d.RecipientId)
            .IsRequired();

        builder.Property(d => d.SenderType)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(d => d.RecipientType)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(d => d.Body)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(d => d.IsRead)
            .IsRequired();

        builder.HasIndex(d => d.SchoolId);
        builder.HasIndex(d => d.StudentId);
        builder.HasIndex(d => new { d.SchoolId, d.StudentId, d.CreatedAt });

        builder.HasOne<School>()
            .WithMany()
            .HasForeignKey(d => d.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Student>()
            .WithMany()
            .HasForeignKey(d => d.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
