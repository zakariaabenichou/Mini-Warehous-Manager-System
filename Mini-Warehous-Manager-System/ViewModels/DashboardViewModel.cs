namespace Mini_Warehous_Manager_System.ViewModels;

public class DashboardViewModel
{
    public int TotalProductsCount { get; set; }
    public int OutOfStockCount { get; set; }
    public int TotalItemsInWarehouse { get; set; }
    public List<ProductListViewModel> RecentProducts { get; set; } = new();
}