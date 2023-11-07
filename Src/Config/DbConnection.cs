using Microsoft.EntityFrameworkCore;
using Models;

public class DbConnection : DbContext {
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Company> Companies { get; set; }
    public string server = Environment.GetEnvironmentVariable("IS_DOCKER") != null ? "postgres" : "localhost"; 

    public override int SaveChanges() {
        AddTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new ()) {
        AddTimestamps();
        return await base.SaveChangesAsync();
    }

    private void AddTimestamps() {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entity in entities)
            {  
                DateTime now = DateTime.UtcNow;

                if (entity.State == EntityState.Added) {
                    ((BaseEntity)entity.Entity).createdAt = now;
                }
                ((BaseEntity)entity.Entity).updatedAt = now;
            }
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
            $"Server={server};" +
            "Port=5432;" +
            "Database=postgres;" +
            "User ID=postgres;" +
            "Password=admin"
        );
}