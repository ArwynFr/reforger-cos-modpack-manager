using ArwynFr.Reforger.ModpackMgr.Domain;

using Microsoft.EntityFrameworkCore;

namespace ArwynFr.Reforger.ModpackMgr.Database;

public class ModsDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : DbContext(dbContextOptions)
{
    private const string ConnectionStringName = "DefaultConnection";
    public DbSet<Mod> Mods { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlite(configuration.GetConnectionString(ConnectionStringName));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
}