namespace ShopManagement.Core.Entities;

/// <summary>
/// Category entity demonstrating Inheritance and Composition
/// </summary>
public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // Composition: Category has many Products
    public ICollection<Product> Products { get; set; } = new List<Product>();
    
    // Method demonstrating business logic
    public int GetProductCount() => Products?.Count ?? 0;
    
    public decimal GetTotalValue()
    {
        return Products?.Sum(p => p.Price * p.StockQuantity) ?? 0;
    }
    
    // Override virtual method (Polymorphism)
    public override string GetDisplayName()
    {
        return $"{Name} ({GetProductCount()} products)";
    }
}