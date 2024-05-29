using DatabaseService.DataModels.Authentication;
using DatabaseService.DataModels.Friends;
using DatabaseService.DataModels.OtpLogs;
using DatabaseService.DataModels.Posts;
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
                ServerVersion.AutoDetect("server=localhost;port=3306;database=socialdb;User=root;password=rootroot")); // Adjust the version to match your MySQL version
        }
    }
        
    public DbSet<Users> Users { get; set; }
    public DbSet<Login> Login { get; set; }
    public DbSet<OtpLog> OtpLogs { get; set; }
    public DbSet<Friendships> FriendShips { get; set; }
    public DbSet<Friend> Friends { get; set; }
    // public DbSet<Posts> Posts { get; set; }
    // public DbSet<PostsLiked> PostsLiked { get; set; }
    // public DbSet<PostsComments> PostsComments { get; set; }
    // public DbSet<PostsShared> PostsShared { get; set; }
    // public DbSet<PostsCommentsLiked> PostsCommentsLikeds { get; set; }
}

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql("server=localhost;port=3306;database=socialdb;user=root;password=rootroot", 
            ServerVersion.AutoDetect("server=localhost;port=3306;database=socialdb;User=root;password=rootroot")); // Adjust the version to match your MySQL version

        return new AppDbContext(optionsBuilder.Options);
    }
}