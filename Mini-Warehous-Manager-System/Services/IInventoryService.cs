using Mini_Warehous_Manager_System.ViewModels;

namespace Mini_Warehous_Manager_System.Services;

public interface IInventoryService
{
    Task<IEnumerable<ProductListViewModel>> GetAllProductsAsync();
    Task<ProductDetailsViewModel?> GetProductDetailsAsync(Guid id);
    Task CreateProductAsync(string sku, string name);
    Task<CreateMovementViewModel?> GetMovementViewModelAsync(Guid id);
    Task ProcessMovementAsync(Guid productId, int quantity, string? reason);
}