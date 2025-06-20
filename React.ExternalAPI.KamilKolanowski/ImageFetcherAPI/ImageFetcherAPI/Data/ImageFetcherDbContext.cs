using ImageFetcherAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageFetcherAPI.Data;

public class ImageFetcherDbContext : DbContext
{
    public ImageFetcherDbContext(DbContextOptions<ImageFetcherDbContext> options)
        : base(options) { }

    public DbSet<Cat> Cats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("TCSA");
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cat>(entity =>
        {
            entity.HasKey(e => e.RowId);
            entity.Property(e => e.RowId).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Id).IsRequired().HasMaxLength(30);
            entity.Property(e => e.Url).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Width).HasMaxLength(6);
            entity.Property(e => e.Height).HasMaxLength(6);
        });
    }
}
