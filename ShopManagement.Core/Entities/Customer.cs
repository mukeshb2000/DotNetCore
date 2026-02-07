namespace ShopManagement.Core.Entities;

/// <summary>
/// Customer entity demonstrating Inheritance and Encapsulation
/// </summary>
public class Customer : BaseEntity
{
    private string _email = string.Empty;
    
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    // Property with validation (Encapsulation)
    public string Email 
    { 
        get => _email;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
                throw new ArgumentException("Invalid email format");
            _email = value;
        }
    }
    
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    
    // Composition: Customer has many Orders
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    
    // Computed property
    public string FullName => $"{FirstName} {LastName}";
    
    // Business logic methods
    public decimal GetTotalOrderValue()
    {
        return Orders?.Sum(o => o.TotalAmount) ?? 0;
    }
    
    public int GetOrderCount() => Orders?.Count ?? 0;
    
    // Override virtual method (Polymorphism)
    public override string GetDisplayName()
    {
        return $"{FullName} ({Email})";
    }
}