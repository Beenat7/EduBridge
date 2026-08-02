using EduBridge.Domain.Schools;
using Microsoft.EntityFrameworkCore;

namespace EduBridge.Infrastructure.Persistence;

public sealed class EduBridgeDbContext : DbContext
{
    public EduBridgeDbContext(DbContextOptions<EduBridgeDbContext> options)
        : base(options)
    {
    }

    public DbSet<School> Schools => Set<School>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(EduBridgeDbContext).Assembly);
    }
}