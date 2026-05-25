using System.ComponentModel.DataAnnotations;

namespace Mini_Warehous_Manager_System.Models;

public class Product
{
    public Guid Id { get; private set; }

    [Required]
    [MaxLength(50)]
    public string SKU { get; private set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; private set; }

    public int CurrentStock { get; private set; }

    
    private Product() { }

    // Erstellung eines neuen Produkts
    public Product(string sku, string name)
    {
        if (string.IsNullOrWhiteSpace(sku))
            throw new ArgumentException("SKU darf nicht leer sein.", nameof(sku));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name darf nicht leer sein.", nameof(name));

        Id = Guid.NewGuid();
        SKU = sku.ToUpper().Trim();
        Name = name.Trim();
        CurrentStock = 0;
    }

    //Bestand einbuchen (Wareneingang)
    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Die Einbuchungsmenge muss größer als 0 sein.", nameof(quantity));

        CurrentStock += quantity;
    }

    // Bestand ausbuchen (Warenausgang)
    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Die Ausbuchungsmenge muss größer als 0 sein.", nameof(quantity));

        if (CurrentStock - quantity < 0)
            throw new InvalidOperationException($"Nicht genügend Bestand für {SKU} auf Lager! Aktuell: {CurrentStock}, Angefordert: {quantity}");

        CurrentStock -= quantity;
    }
}