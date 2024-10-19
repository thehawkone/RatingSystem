using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Rating.Domain.Models;

namespace Rating.DataAccess;

public class RatingDbContext : DbContext
{
    public RatingDbContext(DbContextOptions<RatingDbContext> options)
        : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //User
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);
        
        modelBuilder.Entity<User>()
            .Property(u => u.Name)
            .HasMaxLength(User.MaxUserNameLength)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.PasswordHash)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .IsRequired();
        
        // Product
        modelBuilder.Entity<Product>()
            .HasKey(p => p.ProductId);

        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property(p => p.Description)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property(p => p.Category)
            .IsRequired();
        
        //Review
        modelBuilder.Entity<Review>()
            .HasKey(r => r.ReviewId);

        modelBuilder.Entity<Review>()
            .Property(r => r.UserId)
            .IsRequired();

        modelBuilder.Entity<Review>()
            .Property(r => r.ProductId)
            .IsRequired();

        modelBuilder.Entity<Review>()
            .Property(r => r.Rating)
            .IsRequired();

        modelBuilder.Entity<Review>()
            .Property(r => r.Comment)
            .IsRequired();

        modelBuilder.Entity<Review>()
            .Property(r => r.CreatedAt)
            .IsRequired();
    }
}