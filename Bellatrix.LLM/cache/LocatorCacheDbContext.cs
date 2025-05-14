using Bellatrix.LLM.cache;
using Bellatrix.LLM.Cache;
using Microsoft.EntityFrameworkCore;

namespace Bellatrix.LLM.Cache;
public class LocatorCacheDbContext : DbContext
{
    public DbSet<LocatorCacheEntry> LocatorCache { get; set; }
    public DbSet<SelfHealingLocatorEntry> SelfHealingLocators { get; set; }
    public DbSet<SmartTestExecutionEntry> SmartTestExecutions { get; set; }

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

        modelBuilder.Entity<SelfHealingLocatorEntry>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<SelfHealingLocatorEntry>()
            .HasIndex(e => new { e.Project, e.AppLocation, e.ValidLocator })
            .IsUnique();


        modelBuilder.Entity<SmartTestExecutionEntry>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<SmartTestExecutionEntry>()
            .HasIndex(e => e.TestFullName);
    }
}
