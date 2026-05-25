using System.ComponentModel.DataAnnotations;

namespace Mini_Warehous_Manager_System.Models;

public class StockMovement
{
    public Guid Id { get; private set; }

    [Required]
    public Guid ProductId { get; private set; }

    [Required]
    public int Quantity { get; private set; }

    public DateTime CreatedAt { get; private set; }

    [MaxLength(200)]
    public string? Reason { get; private set; }
    public Product? Product { get; private set; }

    private StockMovement() { }

    public StockMovement(Guid productId, int quantity, string? reason)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Quantity = quantity;
        CreatedAt = DateTime.UtcNow;
        Reason = reason?.Trim();
    }
}