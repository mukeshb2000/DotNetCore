namespace ShopManagement.Core.Entities;

/// <summary>
/// OrderItem entity demonstrating Inheritance and Composition
/// </summary>
public class OrderItem : BaseEntity
{
    private int _quantity;
    private decimal _unitPrice;
    
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    
    // Properties with validation (Encapsulation)
    public int Quantity 
    { 
        get => _quantity;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Quantity must be positive");
            _quantity = value;
            UpdateSubtotal();
        }
    }
    
    public decimal UnitPrice 
    { 
        get => _unitPrice;
        set
        {
            if (value < 0)
                throw new ArgumentException("Unit price cannot be negative");
            _unitPrice = value;
            UpdateSubtotal();
        }
    }
    
    public decimal Subtotal { get; private set; } // Encapsulation: private setter
    
    // Navigation properties
    public Order? Order { get; set; }
    public Product? Product { get; set; }
    
    // Method to calculate subtotal (Encapsulation of business logic)
    public void UpdateSubtotal()
    {
        Subtotal = Quantity * UnitPrice;
        UpdatedAt = DateTime.UtcNow;
    }
    
    // Override virtual method (Polymorphism)
    public override string GetDisplayName()
    {
        return $"{Product?.Name ?? "Unknown"} x{Quantity} = ${Subtotal:F2}";
    }
}