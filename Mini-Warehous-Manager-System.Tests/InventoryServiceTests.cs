using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Mini_Warehous_Manager_System.Data;
using Mini_Warehous_Manager_System.Models;
using Mini_Warehous_Manager_System.Services;
using Xunit;

namespace Mini_Warehous_Manager_System.Tests;

public class InventoryServiceTests
{

    private WmsDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<WmsDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Immer eine neue, leere DB
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)) 
            .Options;

        return new WmsDbContext(options);
    }

    [Fact]
    public async Task ProcessMovement_Should_Increase_Stock_On_Inbound()
    {
        // (Vorbereitung)
        using var context = GetInMemoryDbContext();
        var service = new InventoryService(context);

        //ein Testprodukt
        var product = new Product("TEST-SKU", "Test-Produkt");
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        // (Aktion)
        // buchen 50 Stück ein
        await service.ProcessMovementAsync(product.Id, 50, "Wareneingang Test");

        // ASSERT (Überprüfung)
        var updatedProduct = await context.Products.FindAsync(product.Id);
        Assert.NotNull(updatedProduct);
        Assert.Equal(50, updatedProduct.CurrentStock); // Der Bestand muss jetzt exakt 50 sein!
    }

    [Fact]
    public async Task ProcessMovement_Should_Throw_Exception_When_Stock_Goes_Negative()
    {
        // ARRANGE (Vorbereitung)
        using var context = GetInMemoryDbContext();
        var service = new InventoryService(context);

        // legen ein Produkt an, das bereits 10 Stück auf Lager hat
        var product = new Product("TEST-SKU", "Test-Produkt");
        product.AddStock(10);
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        // ACT & ASSERT (Aktion & Überprüfung)
        // versuchen, 15 Stück abzubuchen (-15). Das muss fehlschlagen!
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await service.ProcessMovementAsync(product.Id, -15, "Warenausgang Test");
        });
    }
}