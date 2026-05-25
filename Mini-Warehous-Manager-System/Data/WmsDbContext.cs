using Microsoft.EntityFrameworkCore;
using Mini_Warehous_Manager_System.Models;

namespace Mini_Warehous_Manager_System.Data;

public class WmsDbContext : DbContext
{
    public WmsDbContext(DbContextOptions<WmsDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<StockMovement> StockMovements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SKU).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CurrentStock).IsRequired();
        });

        modelBuilder.Entity<StockMovement>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Reason).HasMaxLength(200);
            entity.HasOne(e => e.Product)
                  .WithMany()
                  .HasForeignKey(e => e.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}