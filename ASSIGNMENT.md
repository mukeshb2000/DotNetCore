# Shop Management System - .NET Core 8 Assignment

## Overview
This assignment demonstrates a comprehensive Shop Management System built with .NET Core 8, showcasing all major Object-Oriented Programming (OOP) concepts, middleware implementation, service/repository patterns, dependency injection, and CRUD operations.

## Learning Objectives
By completing this assignment, students will understand and implement:

1. **Object-Oriented Programming Concepts**
2. **Middleware Pattern**
3. **Repository Pattern**
4. **Service Layer Pattern**
5. **Dependency Injection**
6. **CRUD Operations**
7. **RESTful API Design**
8. **Entity Framework Core**

## Project Structure

```
ShopManagement/
├── ShopManagement.Core/           # Domain layer (Entities, Interfaces)
├── ShopManagement.Infrastructure/ # Data access layer (Repositories, Services)
├── ShopManagement.API/           # Presentation layer (Controllers, Middleware)
└── ShopManagement.sln           # Solution file
```

## OOP Concepts Demonstrated

### 1. Encapsulation
- **Location**: `ShopManagement.Core/Entities/Product.cs`
- **Example**: Private fields with public properties and validation
```csharp
private decimal _price;
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
```

### 2. Inheritance
- **Location**: All entity classes inherit from `BaseEntity.cs`
- **Example**: `Product : BaseEntity`, `Customer : BaseEntity`
- **Benefits**: Common properties (Id, CreatedAt, UpdatedAt, IsDeleted) shared across all entities

### 3. Polymorphism
- **Location**: Virtual method `GetDisplayName()` in `BaseEntity.cs`
- **Example**: Each entity overrides this method to provide specific display logic
```csharp
// In BaseEntity
public virtual string GetDisplayName() => $"{GetType().Name} - {Id}";

// In Product
public override string GetDisplayName() => $"{Name} - ${Price:F2}";
```

### 4. Abstraction
- **Location**: Interface definitions in `ShopManagement.Core/Interfaces/`
- **Example**: `IRepository<T>`, `IProductService` define contracts without implementation

### 5. Composition and Aggregation
- **Location**: Entity relationships
- **Example**: `Order` has many `OrderItems` (Composition), `Customer` has many `Orders` (Aggregation)

## Design Patterns Implemented

### 1. Repository Pattern
- **Files**: `ShopManagement.Infrastructure/Repositories/`
- **Purpose**: Abstracts data access logic
- **Benefits**: Testability, maintainability, separation of concerns

### 2. Service Layer Pattern
- **Files**: `ShopManagement.Infrastructure/Services/`
- **Purpose**: Contains business logic and orchestrates repository operations
- **Benefits**: Business logic centralization, transaction management

### 3. Middleware Pattern
- **Files**: `ShopManagement.API/Middleware/`
- **Examples**: 
  - `ExceptionHandlingMiddleware`: Global exception handling
  - `RequestLoggingMiddleware`: Request/response logging

## Dependency Injection Examples

### 1. Constructor Injection
```csharp
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }
}
```

### 2. Service Registration
```csharp
// In Program.cs
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
```

## CRUD Operations

### Products API Endpoints
- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Create new product
- `PUT /api/products/{id}` - Update product
- `DELETE /api/products/{id}` - Delete product
- `GET /api/products/category/{categoryId}` - Get products by category
- `GET /api/products/in-stock` - Get in-stock products
- `GET /api/products/low-stock` - Get low-stock products
- `GET /api/products/search?searchTerm=` - Search products
- `PATCH /api/products/{id}/stock` - Update product stock

## Assignment Tasks

### Task 1: Complete the Customer Service (30 points)
Create `CustomerService.cs` in `ShopManagement.Infrastructure/Services/` implementing `ICustomerService`:

```csharp
public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<Customer> CreateCustomerAsync(Customer customer);
    Task<Customer> UpdateCustomerAsync(Customer customer);
    Task<bool> DeleteCustomerAsync(int id);
    Task<Customer?> GetCustomerByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
}
```

**Requirements**:
- Implement all interface methods
- Add proper validation (email format, required fields)
- Use dependency injection for repository access
- Add logging for all operations
- Handle business logic (check email uniqueness)

### Task 2: Create CustomersController (25 points)
Create `CustomersController.cs` in `ShopManagement.API/Controllers/`:

**Requirements**:
- Implement all CRUD operations
- Add proper HTTP status codes
- Include API documentation with XML comments
- Add input validation
- Use dependency injection for service access
- Add proper logging

### Task 3: Implement Order Management (35 points)
Create `OrderService.cs` and `OrdersController.cs`:

**OrderService Requirements**:
- Create, update, delete orders
- Add/remove items from orders
- Calculate order totals
- Update order status
- Validate stock availability before adding items

**OrdersController Requirements**:
- Full CRUD operations for orders
- Get orders by customer
- Get orders by status
- Add/remove items from order endpoints

### Task 4: Add Custom Middleware (10 points)
Create `AuthenticationMiddleware.cs` that:
- Checks for API key in request headers
- Returns 401 Unauthorized if missing
- Logs authentication attempts
- Allows requests to continue if valid

## Running the Application

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or VS Code

### Steps
1. Clone/download the project
2. Open terminal in project root
3. Run: `dotnet restore`
4. Run: `dotnet build`
5. Navigate to API project: `cd ShopManagement.API`
6. Run: `dotnet run`
7. Open browser to: `https://localhost:5001`

### Testing the API
- Swagger UI will be available at the root URL
- Use Postman or curl for API testing
- Sample data is automatically seeded

## Evaluation Criteria

### Code Quality (40%)
- Proper OOP implementation
- Clean code principles
- Proper error handling
- Code documentation

### Architecture (30%)
- Correct implementation of patterns
- Proper separation of concerns
- Dependency injection usage
- Middleware implementation

### Functionality (20%)
- All CRUD operations working
- Business logic implementation
- API endpoints functioning
- Data validation

### Documentation (10%)
- Code comments
- API documentation
- README updates
- Assignment completion notes

## Bonus Points (10 extra points)
- Add unit tests for services
- Implement caching middleware
- Add input validation attributes
- Create custom exceptions
- Add API versioning

## Submission Guidelines
1. Complete all assigned tasks
2. Test all API endpoints
3. Add comments explaining OOP concepts used
4. Create a summary document explaining:
   - Which OOP concepts you implemented and where
   - How dependency injection is used
   - What middleware you created and their purpose
   - Any challenges faced and solutions

## Common Mistakes to Avoid
1. Not using proper dependency injection
2. Putting business logic in controllers
3. Not handling exceptions properly
4. Missing input validation
5. Not following RESTful conventions
6. Forgetting to register services in DI container

## Resources
- [.NET Core Documentation](https://docs.microsoft.com/en-us/dotnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Web API](https://docs.microsoft.com/en-us/aspnet/core/web-api/)
- [Dependency Injection in .NET](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)

## Support
If you encounter issues:
1. Check the error messages carefully
2. Review the existing code for patterns
3. Consult the documentation
4. Ask questions during class or office hours

Good luck with your assignment!