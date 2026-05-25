namespace Mini_Warehous_Manager_System.ViewModels;

public class ProductListViewModel
{
    public Guid Id { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int CurrentStock { get; set; }
}