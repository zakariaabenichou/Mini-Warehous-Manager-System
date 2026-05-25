using System.ComponentModel.DataAnnotations;

namespace Mini_Warehous_Manager_System.ViewModels;

public class CreateMovementViewModel
{
    public Guid ProductId { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int CurrentStock { get; set; }

    [Required(ErrorMessage = "Bitte gib eine Menge an.")]
    [Range(-100000, 100000, ErrorMessage = "Die Menge muss zwischen -100.000 und 100.000 liegen.")]
    public int Quantity { get; set; } 

    [MaxLength(200, ErrorMessage = "Der Grund darf maximal 200 Zeichen lang sein.")]
    public string? Reason { get; set; }
}