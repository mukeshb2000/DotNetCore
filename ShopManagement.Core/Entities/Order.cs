namespace ShopManagement.Core.Entities;

/// <summary>
/// Order entity demonstrating Inheritance, Composition, and Aggregation
/// </summary>
public class Order : BaseEntity
{
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalAmount { get; private set; } // Encapsulation: private setter
    
    // Navigation properties
    public Customer? Customer { get; set; }
    
    // Composition: Order has many OrderItems
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    
    // Method to calculate total (Encapsulation of business logic)
    public void CalculateTotal()
    {
        TotalAmount = OrderItems?.Sum(item => item.Subtotal) ?? 0;
        UpdatedAt = DateTime.UtcNow;
    }
    
    // Method to add item with business logic
    public void AddItem(Product product, int quantity)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));
        
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive");
        
        if (!product.IsInStock() || product.StockQuantity < quantity)
            throw new InvalidOperationException("Insufficient stock");
        
        var existingItem = OrderItems.FirstOrDefault(i => i.ProductId == product.Id);
        
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
            existingItem.UpdateSubtotal();
        }
        else
        {
            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                Product = product,
                Quantity = quantity,
                UnitPrice = product.Price
            };
            orderItem.UpdateSubtotal();
            OrderItems.Add(orderItem);
        }
        
        CalculateTotal();
    }
    
    // Override virtual method (Polymorphism)
    public override string GetDisplayName()
    {
        return $"Order #{Id} - ${TotalAmount:F2} ({Status})";
    }
}

/// <summary>
/// Enum demonstrating encapsulation of related constants
/// </summary>
public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}