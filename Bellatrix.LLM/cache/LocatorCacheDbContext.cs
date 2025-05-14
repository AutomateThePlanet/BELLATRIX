using Bellatrix.LLM.Cache;
using Microsoft.EntityFrameworkCore;

namespace Bellatrix.LLM.Cache;
public class LocatorCacheDbContext : DbContext
{
    public DbSet<LocatorCacheEntry> LocatorCache { get; set; }

    private readonly string _connectionString;

    public LocatorCacheDbContext(string connectionString)
    {
        try
        {
            _connectionString = connectionString;
            Database.EnsureCreated(); // Code-first init
        }
        catch (Exception ex)
        {
            Logger.LogError($"❌ Failed to initialize LocatorCacheDbContext: {ex.Message}");
        }

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LocatorCacheEntry>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<LocatorCacheEntry>()
            .HasIndex(e => new { e.Project, e.Instruction })
            .IsUnique();
    }
}
