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
            optionsBuilder.UseMySql("server=social-backend-db;port=3307;database=socialdb;User=socialuser;password=socialpassword;", 
                ServerVersion.AutoDetect("server=localhost;port=3307;database=socialdb;User=socialuser;password=socialpassword")); // Adjust the version to match your MySQL version
        }
    }
        
    public DbSet<Users> Users { get; set; }
    public DbSet<Login> Login { get; set; }
}
