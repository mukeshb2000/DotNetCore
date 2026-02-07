# Object-Oriented Programming Concepts
## PowerPoint Presentation Content for Shop Management System

---

## Slide 1: Title Slide
**Object-Oriented Programming in .NET Core 8**
*Using Shop Management System*

- **Course**: .NET Core 8 Development
- **Topic**: OOP Concepts with Real-World Examples
- **Project**: Shop Management System
- **Instructor**: [Your Name]
- **Date**: [Current Date]

---

## Slide 2: Learning Objectives
**What You Will Learn Today**

By the end of this session, you will understand:
- ✅ **Encapsulation** - Data hiding and protection
- ✅ **Inheritance** - Code reuse and hierarchies
- ✅ **Polymorphism** - One interface, multiple implementations
- ✅ **Abstraction** - Hiding complexity through interfaces
- ✅ **Real-world application** in .NET Core projects

---

## Slide 3: OOP Overview
**The Four Pillars of Object-Oriented Programming**

```
┌─────────────────┐    ┌─────────────────┐
│   ENCAPSULATION │    │   INHERITANCE   │
│   Data Hiding   │    │   Code Reuse    │
└─────────────────┘    └─────────────────┘
         │                       │
         └───────┬───────────────┘
                 │
┌─────────────────┐    ┌─────────────────┐
│   POLYMORPHISM  │    │   ABSTRACTION   │
│ Multiple Forms  │    │ Hide Complexity │
└─────────────────┘    └─────────────────┘
```

**Why OOP?**
- **Modularity**: Easier to maintain and debug
- **Reusability**: Write once, use many times
- **Scalability**: Easy to extend and modify
- **Real-world modeling**: Maps to business concepts

---

## Slide 4: Our Shop Management System
**Real-World Business Model**

```
🏪 SHOP MANAGEMENT SYSTEM
├── 📦 Products (Laptop, Phone, T-Shirt)
├── 📂 Categories (Electronics, Clothing, Books)
├── 👥 Customers (John, Sarah, Mike)
├── 🛒 Orders (Customer purchases)
└── 📋 Order Items (Products in orders)
```

**Perfect for Learning OOP Because:**
- Real business relationships
- Complex data interactions
- Multiple entity types
- Business rules and validation

---

## Slide 5: Encapsulation - Definition
**Encapsulation: "Data Hiding and Protection"**

**What is Encapsulation?**
- Bundling data and methods together
- Hiding internal implementation details
- Controlling access to data through properties
- Protecting data integrity with validation

**Key Benefits:**
- **Security**: Data cannot be accessed directly
- **Validation**: Control what values are allowed
- **Maintenance**: Change implementation without affecting users
- **Debugging**: Easier to track data changes

---

## Slide 6: Encapsulation - Code Example
**Product.cs - Real Implementation**

```csharp
public class Product : BaseEntity
{
    // Private fields (Hidden from outside)
    private decimal _price;
    private int _stockQuantity;

    // Public property with validation (Controlled access)
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

    public int StockQuantity 
    { 
        get => _stockQuantity;
        set
        {
            if (value < 0)
                throw new ArgumentException("Stock cannot be negative");
            _stockQuantity = value;
        }
    }

    // Business logic method (Encapsulated behavior)
    public bool IsInStock() => StockQuantity > 0;
}
```

---

## Slide 7: Encapsulation - Benefits in Action
**Why This Matters**

**❌ Without Encapsulation:**
```csharp
var product = new Product();
product._price = -100;  // Invalid! But no protection
product._stockQuantity = -50;  // Invalid! But allowed
```

**✅ With Encapsulation:**
```csharp
var product = new Product();
product.Price = -100;  // Throws exception - protected!
product.StockQuantity = -50;  // Throws exception - validated!

// Safe operations
product.Price = 99.99;  // Valid - accepted
bool available = product.IsInStock();  // Business logic
```

**Real-World Impact:**
- Prevents invalid data entry
- Maintains business rules
- Easier debugging and maintenance

---

## Slide 8: Inheritance - Definition
**Inheritance: "Code Reuse Through Hierarchies"**

**What is Inheritance?**
- Child classes inherit properties and methods from parent classes
- Promotes code reuse and reduces duplication
- Creates "is-a" relationships
- Enables hierarchical organization

**Key Benefits:**
- **Code Reuse**: Write common code once
- **Consistency**: All entities share common behavior
- **Maintainability**: Update base class affects all children
- **Organization**: Logical hierarchy structure

---

## Slide 9: Inheritance - Code Example
**BaseEntity.cs - Parent Class**

```csharp
// Base class with common properties
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    
    // Virtual method for polymorphism
    public virtual string GetDisplayName()
    {
        return $"{GetType().Name} - {Id}";
    }
}

// Child classes inherit from BaseEntity
public class Product : BaseEntity  // Product IS-A BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    // Inherits: Id, CreatedAt, UpdatedAt, IsDeleted, GetDisplayName()
}

public class Customer : BaseEntity  // Customer IS-A BaseEntity
{
    public string FirstName { get; set; }
    public string Email { get; set; }
    // Inherits: Id, CreatedAt, UpdatedAt, IsDeleted, GetDisplayName()
}
```

---

## Slide 10: Inheritance - Hierarchy Visualization
**Our Entity Hierarchy**

```
                    BaseEntity
                   ┌─────────────┐
                   │ Id          │
                   │ CreatedAt   │
                   │ UpdatedAt   │
                   │ IsDeleted   │
                   │ GetDisplayName() │
                   └─────────────┘
                         │
        ┌────────────────┼────────────────┐
        │                │                │
   ┌─────────┐    ┌─────────────┐   ┌─────────┐
   │ Product │    │  Customer   │   │  Order  │
   │ Name    │    │ FirstName   │   │ OrderDate│
   │ Price   │    │ Email       │   │ Status   │
   │ Stock   │    │ Phone       │   │ Total    │
   └─────────┘    └─────────────┘   └─────────┘
```

**Benefits:**
- All entities automatically have Id, timestamps, soft delete
- Consistent behavior across all entities
- Easy to add new entity types

---

## Slide 11: Polymorphism - Definition
**Polymorphism: "One Interface, Multiple Forms"**

**What is Polymorphism?**
- Same method name, different implementations
- Objects of different types respond to same interface
- Enables flexible and extensible code
- "Many forms" of the same behavior

**Types:**
- **Method Overriding**: Child class provides specific implementation
- **Interface Implementation**: Different classes implement same interface
- **Method Overloading**: Same method name, different parameters

---

## Slide 12: Polymorphism - Code Example
**GetDisplayName() - Different Implementations**

```csharp
// Base implementation
public abstract class BaseEntity
{
    public virtual string GetDisplayName()
    {
        return $"{GetType().Name} - {Id}";
    }
}

// Each child class overrides with specific logic
public class Product : BaseEntity
{
    public override string GetDisplayName()
    {
        return $"{Name} - ${Price:F2}";  // "Laptop - $999.99"
    }
}

public class Customer : BaseEntity
{
    public override string GetDisplayName()
    {
        return $"{FullName} ({Email})";  // "John Doe (john@email.com)"
    }
}

public class Order : BaseEntity
{
    public override string GetDisplayName()
    {
        return $"Order #{Id} - ${TotalAmount:F2} ({Status})";
    }
}
```

---

## Slide 13: Polymorphism - In Action
**Same Method, Different Results**

```csharp
// Polymorphism in action
List<BaseEntity> entities = new List<BaseEntity>
{
    new Product { Name = "Laptop", Price = 999.99m },
    new Customer { FirstName = "John", Email = "john@email.com" },
    new Order { TotalAmount = 1299.99m, Status = OrderStatus.Shipped }
};

// Same method call, different implementations
foreach (BaseEntity entity in entities)
{
    Console.WriteLine(entity.GetDisplayName());
}

// Output:
// Laptop - $999.99
// John Doe (john@email.com)
// Order #123 - $1299.99 (Shipped)
```

**Power of Polymorphism:**
- Write code once, works with all types
- Easy to add new entity types
- Flexible and maintainable code

---

## Slide 14: Abstraction - Definition
**Abstraction: "Hide Complexity, Show Essentials"**

**What is Abstraction?**
- Hide implementation details from users
- Show only essential features and operations
- Define contracts without implementation
- Simplify complex systems

**Achieved Through:**
- **Interfaces**: Define what an object can do
- **Abstract Classes**: Partial implementation
- **Encapsulation**: Hide internal workings

**Benefits:**
- **Simplicity**: Users don't need to know how it works
- **Flexibility**: Change implementation without affecting users
- **Testability**: Easy to mock and test
- **Maintainability**: Loose coupling between components

---

## Slide 15: Abstraction - Interface Example
**IRepository<T> - Abstract Contract**

```csharp
// Abstract interface - defines WHAT, not HOW
public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    // No implementation details - just the contract
}

// Concrete implementation - defines HOW
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ShopDbContext _context;
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().Where(e => !e.IsDeleted).ToListAsync();
        // Implementation hidden from users
    }
    
    // Other methods implemented...
}
```

---

## Slide 16: Abstraction - Benefits
**Why Abstraction Matters**

**Using the Interface (Simple):**
```csharp
public class ProductService
{
    private readonly IProductRepository _repository;  // Abstract dependency
    
    public ProductService(IProductRepository repository)
    {
        _repository = repository;  // Don't care about implementation
    }
    
    public async Task<Product> GetProductAsync(int id)
    {
        return await _repository.GetByIdAsync(id);  // Simple call
        // Don't need to know: Database type, connection strings, 
        // SQL queries, caching, etc.
    }
}
```

**Benefits:**
- **Simple to Use**: Just call methods, don't worry about complexity
- **Testable**: Can mock IProductRepository for unit tests
- **Flexible**: Can switch from SQL to MongoDB without changing service
- **Maintainable**: Repository changes don't affect service code

---

## Slide 17: All OOP Concepts Together
**Shop Management System - Complete Picture**

```csharp
// ABSTRACTION: Interface defines contract
public interface IProductService
{
    Task<Product> CreateProductAsync(Product product);
}

// INHERITANCE: ProductService inherits behavior patterns
public class ProductService : IProductService
{
    // ENCAPSULATION: Private field, controlled access
    private readonly IProductRepository _repository;
    
    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Product> CreateProductAsync(Product product)
    {
        // ENCAPSULATION: Validation protects data integrity
        if (product.Price < 0)
            throw new ArgumentException("Price cannot be negative");
            
        return await _repository.CreateAsync(product);
    }
}

// POLYMORPHISM: Different products, same interface
var laptop = new Product { Name = "Laptop" };
var phone = new Product { Name = "Phone" };

// Both respond to same method, different results
Console.WriteLine(laptop.GetDisplayName());  // "Laptop - $999.99"
Console.WriteLine(phone.GetDisplayName());   // "Phone - $699.99"
```

---

## Slide 18: Real-World Benefits
**Why OOP Matters in Professional Development**

**🏢 Enterprise Applications:**
- **Maintainability**: Easy to modify and extend
- **Team Collaboration**: Clear contracts and responsibilities
- **Code Reuse**: Don't repeat yourself (DRY principle)
- **Testing**: Mock interfaces for unit testing

**💼 Business Value:**
- **Faster Development**: Reuse existing components
- **Lower Costs**: Less code to maintain
- **Better Quality**: Encapsulation prevents bugs
- **Scalability**: Easy to add new features

**🔧 Technical Benefits:**
- **Loose Coupling**: Components don't depend on implementations
- **High Cohesion**: Related functionality grouped together
- **Separation of Concerns**: Each class has single responsibility
- **Design Patterns**: Foundation for advanced patterns

---

## Slide 19: Common Mistakes to Avoid
**OOP Anti-Patterns**

**❌ Breaking Encapsulation:**
```csharp
// BAD: Public fields
public class Product
{
    public decimal price;  // Anyone can set invalid values
}

// GOOD: Encapsulated properties
public class Product
{
    private decimal _price;
    public decimal Price 
    { 
        get => _price;
        set => _price = value < 0 ? 0 : value;  // Validation
    }
}
```

**❌ Inappropriate Inheritance:**
```csharp
// BAD: Car IS-A Engine (wrong relationship)
public class Car : Engine { }

// GOOD: Car HAS-A Engine (composition)
public class Car 
{
    private Engine _engine;  // Composition, not inheritance
}
```

**❌ Interface Pollution:**
```csharp
// BAD: Too many responsibilities
public interface IGodInterface
{
    void SaveToDatabase();
    void SendEmail();
    void GenerateReport();
    void ProcessPayment();
}

// GOOD: Single responsibility
public interface IRepository { void Save(); }
public interface IEmailService { void SendEmail(); }
```

---

## Slide 20: Hands-On Exercise
**Your Turn: Complete the Assignment**

**🎯 Assignment Tasks:**

1. **CustomerService Implementation** (30 points)
   - Apply encapsulation in validation methods
   - Use inheritance from base service patterns
   - Implement abstraction through ICustomerService

2. **CustomersController** (25 points)
   - Demonstrate polymorphism in error handling
   - Use dependency injection (abstraction)
   - Apply encapsulation in request/response handling

3. **Order Management** (35 points)
   - Complex inheritance hierarchies
   - Business logic encapsulation
   - Interface abstraction for services

4. **Custom Middleware** (10 points)
   - Abstraction through middleware pipeline
   - Encapsulation of cross-cutting concerns

---

## Slide 21: Best Practices
**Professional OOP Guidelines**

**✅ DO:**
- **Favor Composition over Inheritance** when possible
- **Program to Interfaces, not Implementations**
- **Keep Classes Small and Focused** (Single Responsibility)
- **Use Meaningful Names** for classes and methods
- **Validate Input** in property setters
- **Document Public APIs** with XML comments

**❌ DON'T:**
- **Expose Internal State** through public fields
- **Create Deep Inheritance Hierarchies** (max 3-4 levels)
- **Put Business Logic in Controllers** (use services)
- **Ignore Exception Handling** in encapsulated methods
- **Create God Classes** with too many responsibilities

**🎯 Remember:**
- **SOLID Principles** guide good OOP design
- **Design Patterns** solve common problems
- **Clean Code** is more important than clever code

---

## Slide 22: Next Steps
**Continuing Your OOP Journey**

**🚀 Advanced Topics to Explore:**
- **SOLID Principles** (Single Responsibility, Open/Closed, etc.)
- **Design Patterns** (Factory, Observer, Strategy, etc.)
- **Dependency Injection** (Constructor, Property, Method injection)
- **Unit Testing** with Mocking frameworks
- **Clean Architecture** patterns

**📚 Recommended Learning:**
- Complete the Shop Management assignment
- Explore Entity Framework relationships
- Study ASP.NET Core middleware pipeline
- Practice with different design patterns
- Build your own projects applying OOP

**💡 Key Takeaway:**
*OOP is not just about syntax - it's about thinking in terms of objects, responsibilities, and relationships that model real-world problems.*

---

## Slide 23: Q&A and Discussion
**Questions & Answers**

**Common Questions:**
- When should I use inheritance vs composition?
- How do I know if my class is well-encapsulated?
- What's the difference between abstract classes and interfaces?
- How does polymorphism help in real projects?
- When is abstraction too much abstraction?

**Discussion Points:**
- Share examples from your own experience
- Discuss challenges in the assignment
- Explore real-world applications
- Plan next learning steps

**Resources:**
- Assignment files and documentation
- Code examples in the project
- Additional reading materials
- Office hours for individual help

---

## Slide 24: Summary
**Key Takeaways**

**🎯 The Four Pillars:**
- **Encapsulation**: Protect and validate your data
- **Inheritance**: Reuse code through hierarchies  
- **Polymorphism**: Same interface, different behaviors
- **Abstraction**: Hide complexity, show essentials

**💼 Professional Impact:**
- Write maintainable, scalable code
- Work effectively in teams
- Build robust applications
- Follow industry best practices

**🚀 Your Next Steps:**
1. Complete the Shop Management assignment
2. Apply OOP concepts in your own projects
3. Study advanced design patterns
4. Practice, practice, practice!

**Remember: Good OOP design makes complex systems simple to understand and maintain.**

---

*End of Presentation*