namespace Mini_Warehous_Manager_System.ViewModels;

public class ProductDetailsViewModel
{
    public Guid Id { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int CurrentStock { get; set; }
    public List<MovementItemViewModel> Movements { get; set; } = new();
}

public class MovementItemViewModel
{
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Reason { get; set; }
}