namespace ShopManagement.Core.Entities;

/// <summary>
/// Product entity demonstrating Encapsulation and Inheritance
/// </summary>
public class Product : BaseEntity
{
    private decimal _price;
    private int _stockQuantity;

    // Encapsulation: Private fields with public properties
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // Property with validation (Encapsulation)
    public decimal Price 
    { 
        get => _price;
        set
        {
            if (value < 0)
                throw new ArgumentException("Price cannot be negative");
            _price = value;
        }
    }
    
    // Property with validation (Encapsulation)
    public int StockQuantity 
    { 
        get => _stockQuantity;
        set
        {
            if (value < 0)
                throw new ArgumentException("Stock quantity cannot be negative");
            _stockQuantity = value;
        }
    }
    
    public int CategoryId { get; set; }
    
    // Navigation property
    public Category? Category { get; set; }
    
    // Method demonstrating business logic encapsulation
    public bool IsInStock() => StockQuantity > 0;
    
    public void UpdateStock(int quantity)
    {
        if (StockQuantity + quantity < 0)
            throw new InvalidOperationException("Insufficient stock");
        
        StockQuantity += quantity;
        UpdatedAt = DateTime.UtcNow;
    }
    
    // Override virtual method (Polymorphism)
    public override string GetDisplayName()
    {
        return $"{Name} - ${Price:F2}";
    }
}