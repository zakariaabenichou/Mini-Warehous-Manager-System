using Microsoft.EntityFrameworkCore;
using Mini_Warehous_Manager_System.Data;
using Mini_Warehous_Manager_System.Models;
using Mini_Warehous_Manager_System.ViewModels;

namespace Mini_Warehous_Manager_System.Services;

public class InventoryService : IInventoryService
{
    private readonly WmsDbContext _context;

    public InventoryService(WmsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductListViewModel>> GetAllProductsAsync()
    {
        return await _context.Products
            .Select(p => new ProductListViewModel
            {
                Id = p.Id,
                SKU = p.SKU,
                Name = p.Name,
                CurrentStock = p.CurrentStock
            })
            .ToListAsync();
    }

    public async Task<ProductDetailsViewModel?> GetProductDetailsAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return null;

        var movements = await _context.StockMovements
            .Where(m => m.ProductId == id)
            .OrderByDescending(m => m.CreatedAt)
            .Select(m => new MovementItemViewModel
            {
                Quantity = m.Quantity,
                CreatedAt = m.CreatedAt,
                Reason = m.Reason
            })
            .ToListAsync();

        return new ProductDetailsViewModel
        {
            Id = product.Id,
            SKU = product.SKU,
            Name = product.Name,
            CurrentStock = product.CurrentStock,
            Movements = movements
        };
    }

    public async Task CreateProductAsync(string sku, string name)
    {
        var product = new Product(sku, name);
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task<CreateMovementViewModel?> GetMovementViewModelAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return null;

        return new CreateMovementViewModel
        {
            ProductId = product.Id,
            SKU = product.SKU,
            Name = product.Name,
            CurrentStock = product.CurrentStock
        };
    }

    public async Task ProcessMovementAsync(Guid productId, int quantity, string? reason)
    {
        if (quantity == 0)
            throw new ArgumentException("Die Menge darf nicht 0 sein.");

        // eine Datenbank-Transaktion
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new KeyNotFoundException("Produkt nicht gefunden.");

            if (quantity > 0)
            {
                product.AddStock(quantity);
            }
            else
            {
                product.RemoveStock(Math.Abs(quantity)); 
            }

            var movement = new StockMovement(product.Id, quantity, reason);

            _context.Products.Update(product);
            await _context.StockMovements.AddAsync(movement);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw; 
        }
    }
}