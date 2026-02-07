namespace ShopManagement.Core.Entities;

/// <summary>
/// Base entity demonstrating Inheritance (OOP Concept)
/// All entities inherit common properties from this base class
/// </summary>
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    
    // Virtual method demonstrating Polymorphism
    public virtual string GetDisplayName()
    {
        return $"{GetType().Name} - {Id}";
    }
}