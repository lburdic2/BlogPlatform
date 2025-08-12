namespace blog;
using Microsoft.EntityFrameworkCore;

public class BlogContext : DbContext
{
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProfile>()
        .HasMany(u => u.Blogs)
        .WithOne(b => b.UserProfile)
        .HasForeignKey(b => b.UserProfileId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Blog>()
        .HasMany(b => b.Comments)
        .WithOne(c => c.Blog)
        .HasForeignKey(c => c.BlogId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
        .HasOne(c => c.UserProfile)
        .WithMany()
        .HasForeignKey(c => c.UserProfileId)
        .OnDelete(DeleteBehavior.Cascade);
    }


}