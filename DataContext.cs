using Microsoft.EntityFrameworkCore;

namespace AucSite;
 
public class ApplicationContext : DbContext {
    public DbSet<User>? Users { get; set; };
 
    public ApplicationContext() {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=пароль_от_postgres");
    }
}