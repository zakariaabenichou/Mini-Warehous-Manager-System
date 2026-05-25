using Microsoft.AspNetCore.Mvc;
using Mini_Warehous_Manager_System.Services;
using Mini_Warehous_Manager_System.ViewModels;

namespace Mini_warehous_manager_system.Controllers;

public class ProductsController : Controller
{
    private readonly IInventoryService _inventoryService;

    // Dependency Injection
    public ProductsController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    // GET: /Products
    public async Task<IActionResult> Index()
    {
        var products = await _inventoryService.GetAllProductsAsync();
        return View(products);
    }

    // GET: /Products/Details/{id}
    public async Task<IActionResult> Details(Guid id)
    {
        var details = await _inventoryService.GetProductDetailsAsync(id);
        if (details == null)
        {
            return NotFound();
        }
        return View(details);
    }

    // GET: /Products/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string sku, string name)
    {
        if (string.IsNullOrWhiteSpace(sku) || string.IsNullOrWhiteSpace(name))
        {
            ModelState.AddModelError("", "SKU und Name dürfen nicht leer sein.");
            return View();
        }

        try
        {
            await _inventoryService.CreateProductAsync(sku, name);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Fehler beim Erstellen des Produkts: {ex.Message}");
            return View();
        }
    }

    // GET: /Products/ManageStock/{id}
    public async Task<IActionResult> ManageStock(Guid id)
    {
        var viewModel = await _inventoryService.GetMovementViewModelAsync(id);
        if (viewModel == null)
        {
            return NotFound();
        }
        return View(viewModel);
    }

    // POST: /Products/ManageStock
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ManageStock(CreateMovementViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await _inventoryService.ProcessMovementAsync(model.ProductId, model.Quantity, model.Reason);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            // Bestand darf nicht negativ werden
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Ein unerwarteter Fehler ist aufgetreten: {ex.Message}");
            return View(model);
        }
    }
}