using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mini_Warehous_Manager_System.Data;
using Mini_Warehous_Manager_System.ViewModels;

namespace Mini_Warehous_Manager_System.Controllers;

public class HomeController : Controller
{
    private readonly WmsDbContext _context;

    // Wir holen uns den Datenbank-Kontext, um Live-Statistiken zu berechnen
    public HomeController(WmsDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // 1. Berechne Statistiken direkt aus der SQL-Datenbank
        var totalProducts = await _context.Products.CountAsync();
        var outOfStock = await _context.Products.CountAsync(p => p.CurrentStock == 0);
        var totalItems = await _context.Products.SumAsync(p => p.CurrentStock);

        // 2. Lade die 3 neuesten Produkte für das Dashboard
        var recentProducts = await _context.Products
            .OrderByDescending(p => p.Id)
            .Take(3)
            .Select(p => new ProductListViewModel
            {
                Id = p.Id,
                SKU = p.SKU,
                Name = p.Name,
                CurrentStock = p.CurrentStock
            })
            .ToListAsync();

        // 3. ViewModel befüllen
        var dashboardData = new DashboardViewModel
        {
            TotalProductsCount = totalProducts,
            OutOfStockCount = outOfStock,
            TotalItemsInWarehouse = totalItems,
            RecentProducts = recentProducts
        };

        return View(dashboardData);
    }
}