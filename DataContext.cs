using Microsoft.EntityFrameworkCore;

namespace AucSite;
 
public class ApplicationContext : DbContext {
    public DbSet<User>? Users { get; set; }
    public DbSet<Auction>? Auctions { get; set; }
    public DbSet<Lot>? Lots { get; set; }
 
    public ApplicationContext() {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=SecurityBD;Username=postgres;Password=admin");
    }
}