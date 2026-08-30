using EduBridge.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EduBridge.Infrastructure.Persistence;

public sealed class EduBridgeDbContext : IdentityDbContext<EduBridgeUser, IdentityRole<Guid>, Guid>
{
    public EduBridgeDbContext(DbContextOptions<EduBridgeDbContext> options)
        : base(options)
    {
    }

    public DbSet<School> Schools => Set<School>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Parent> Parents => Set<Parent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(EduBridgeDbContext).Assembly);
    }
}