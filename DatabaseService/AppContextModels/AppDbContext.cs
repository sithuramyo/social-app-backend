using DatabaseService.DataModels.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace DatabaseService.AppContextModels;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("server=localhost;port=3306;database=socialdb;User=root;password=rootroot;", 
                ServerVersion.AutoDetect("server=localhost;port=3306;database=socialdb;User=root;password=rootroot;")); // Adjust the version to match your MySQL version
        }
    }
        
    public DbSet<Users> Users { get; set; }
    public DbSet<Login> Login { get; set; }
}

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql("server=localhost;port=3306;database=socialdb;user=root;password=rootroot", 
            new MySqlServerVersion(new Version(8, 0, 34))); // Adjust the version to match your MySQL version

        return new AppDbContext(optionsBuilder.Options);
    }
}